using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FireSkull : MonoBehaviour
{
    public Transform target;

    public float speed;
    public int health;
    public static int damage = 30; 
    public float nextPointDistance;
    public Transform FireSkullGFX;

    GameObject projectile;
    Path path;

    int currentWaypoint = 0;
    bool reachedEnd = false;

    Seeker seeker;
    Rigidbody2D rb;
    Animator animator;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, .5f);
        animator = GetComponentInChildren<Animator>();
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 2;
        }
    }

    void Update()
    {
        if (path == null)
        {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEnd = true;
            return;
        }
        else
        {
            reachedEnd = true;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        Vector2 force = direction * speed * Time.deltaTime;
        rb.AddForce(force);
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextPointDistance)
        {
            currentWaypoint++;
        }
        if (rb.velocity.x >= 0.01f)
        {
            FireSkullGFX.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (rb.velocity.x <= -0.01f)
        {
            FireSkullGFX.localScale = new Vector3(1f, 1f, 1f);
        }
    }
    void TakeDamage()
    {
        health -= PlayerMovement.ATTACK;
        if (health <= 0)
        {
            SoundManager.PlaySound("kill");
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (health > 0)
        {
            if (other.gameObject.tag == "PlayerSword")
            {
                TakeDamage();
            }
        }
    }
}
