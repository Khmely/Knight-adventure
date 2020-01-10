using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Ghost_v2 : MonoBehaviour
{
    public Transform target;

    public float speed = 15f;
    public float nextPointDistance = 3f;
    public bool turnRight = false;
    public Transform GhostGFX;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject projectile;

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
    }

    void UpdatePath() {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p) {
        if (!p.error) {
            path = p;
            currentWaypoint = 0;
        }
    }

    void Update()
    {
        if (path == null) {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEnd = true;
            return;
        }
        else {
            reachedEnd = true;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        Vector2 force = direction * speed * Time.deltaTime;
        rb.AddForce(force);
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextPointDistance) {
            currentWaypoint++;
        }
        if (rb.velocity.x >= 0.01f)
        {
            GhostGFX.localScale = new Vector3(-1f, 1f, 1f);
            turnRight = false;
        }
        else if (rb.velocity.x <= -0.01f)
        {
            GhostGFX.localScale = new Vector3(1f, 1f, 1f);
            turnRight = true;
        }

        if (timeBtwShots <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
    public bool CheckIsRight()
    {
        return turnRight;
    }

}
