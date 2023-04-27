using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    public Texture2D cursorShooting;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<MainHero>() != null)
        {
            Cursor.SetCursor(cursorShooting, Vector2.zero, CursorMode.Auto);

            FindObjectOfType<Game>().canShoot = true;
            Destroy(gameObject);
        }
    }
}
