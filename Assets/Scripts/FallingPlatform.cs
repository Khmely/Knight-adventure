using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void dropPlatform() {
        rb.isKinematic = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player")) {
            Invoke("dropPlatform", 0.5f);
            Destroy(gameObject, 2f);
        }
    }
}
