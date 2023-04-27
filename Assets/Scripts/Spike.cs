using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private Game game;

    private void Awake()
    {
        game = FindObjectOfType<Game>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<MainHero>() != null)
        {
            game.GameOver(GetComponent<DangerObject>().danger);
        }
    }
}
