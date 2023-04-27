using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<MainHero>() != null)
        {
            Debug.Log("К следующему уровню!");
        }
    }
}
