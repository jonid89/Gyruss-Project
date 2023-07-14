using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFieldCenter : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shot"))
        {
            collision.gameObject.SetActive(false);
        }
    }

}
