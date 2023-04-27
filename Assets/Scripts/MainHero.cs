using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHero : MonoBehaviour
{
    private Game game;

    public Controls controls;
    private float movement;

    private Rigidbody2D rb2d;

    [Header("Stats")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    [Header("Body Renderer")]
    [SerializeField] private SpriteRenderer body;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;

    [Header("Fall Damage")]
    [SerializeField] private float maxFlyingTime;
    private float timer;

    [SerializeField] private bool facingRight;

    private Animator animator;
    [HideInInspector] public ParticleSystem particles;

    private void OnEnable()
    {
        controls.Enable();
    }

    private void Awake()
    {
        controls = new Controls();

        game = FindObjectOfType<Game>();

        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        particles = GetComponent<ParticleSystem>();

        controls.Player.Jump.performed += _ => Jump();

        body.color = Random.ColorHSV();

        timer = maxFlyingTime;
    }

    private void Update()
    {
        movement = controls.Player.Movement.ReadValue<float>();

        transform.Translate(new Vector2(movement * speed * Time.deltaTime, 0));

        if (!GroundCheck())
        {
            timer -= Time.deltaTime;
        }
        else
        {
            if (timer <= 0)
            {
                game.GameOver(DangerObject.Danger.falling);
                timer = maxFlyingTime;
            }
            else
            {
                timer = maxFlyingTime;
            }
        }

        if (movement < 0 && facingRight ||
            movement > 0 && !facingRight)
        {
            Flip();
        }

        animator.SetFloat("Speed", Mathf.Abs(movement));
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Jump()
    {
        if (GroundCheck())
        {
            rb2d.AddForce(Vector2.up * jumpForce);
        }
    }

    private bool GroundCheck()
    {
        Collider2D ground = Physics2D.OverlapCircle(groundCheck.position, .1f, whatIsGround);

        if (ground != null)
        {
            return true;
        }

        return false;
    }

    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
