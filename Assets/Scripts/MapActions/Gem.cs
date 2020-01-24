using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    int value = 50;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player Main Collider")
        {
            GameManager.score += value;
            Destroy(gameObject);
        }
    }
}
