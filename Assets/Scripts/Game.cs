using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    private MainHero hero;

    private Progress progressBar;
    private Shake shake;

    [SerializeField] private Animator restartAnimator;

    [SerializeField] private int amountOfDanger;
    private static int interactedDanger;
    public static bool spikeDeath = false;
    public static bool fallDeath = false;
    public static bool shootDeath = false;

    [SerializeField] private List<GameObject> spikes;
    [SerializeField] private List<GameObject> fallingObj;
    [SerializeField] private List<GameObject> shootingObj;

    [SerializeField] private GameObject finish;

    [HideInInspector] public bool canShoot;

    private void Awake()
    {
        progressBar = FindObjectOfType<Progress>();
        hero = FindObjectOfType<MainHero>();
        shake = FindObjectOfType<Shake>();

        if (spikeDeath)
        {
            DestroyDanger(spikes);
        }

        if (fallDeath)
        {
            DestroyDanger(fallingObj);
        }

        if (shootDeath)
        {
            DestroyDanger(shootingObj);
        }

        if (interactedDanger == amountOfDanger)
        {
            finish.SetActive(true);
        }

        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    private void Start()
    {
        hero.controls.Player.Shoot.performed += _ => Shoot();
    }

    public void GameOver(DangerObject.Danger danger)
    {
        shake.ShakeCamera();

        hero.particles.Play();
        hero.controls.Disable();

        if (danger == DangerObject.Danger.spike)
        {
            spikeDeath = true;
        }

        if (danger == DangerObject.Danger.falling)
        {
            fallDeath = true;
        }

        if (danger == DangerObject.Danger.shoot)
        {
            shootDeath = true;
        }

        restartAnimator.SetTrigger("Restart");
        interactedDanger++;
        StartCoroutine(RestartLevel());
    }

    private IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(1f);

        progressBar.EditProgress(amountOfDanger);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void DestroyDanger(List<GameObject> danger)
    {
        foreach(GameObject obj in danger)
        {
            Destroy(obj);
        }
    }

    private void Shoot()
    {
        if (canShoot)
        {
            Vector2 mousePosition = hero.controls.Player.MousePosition.ReadValue<Vector2>();
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            RaycastHit2D hitInfo = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (hitInfo.collider != null)
            {
                if (hitInfo.collider.gameObject.GetComponent<MainHero>() != null)
                {
                    GameOver(DangerObject.Danger.shoot);
                }
            }
        }
    }
}
