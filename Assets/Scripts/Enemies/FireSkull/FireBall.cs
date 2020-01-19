using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed;

    private Transform player;
    private Vector2 target;
    private Animator animator;
    SpriteRenderer sr;
    PlayerMovement pm;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        target = new Vector2(player.position.x, player.position.y);
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (target.x > transform.position.x)
        {
            sr.flipX = false;
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        else {
            sr.flipX = true;
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }

        if (transform.position.x == target.x && transform.position.y == target.y) {
            DestroyFireBall();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player") {
            DestroyFireBall();
        }
    }

    void DestroyFireBall() {
        animator.SetTrigger("Die");
        Destroy(gameObject, 0.5f);
    }


}
