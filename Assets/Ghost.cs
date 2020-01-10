using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Ghost : MonoBehaviour
{
    public float speed;
    public float stoppingDistane;
    public float retreatDistane;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject projectile;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) > stoppingDistane) {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        } else if (Vector2.Distance(transform.position, player.position) < stoppingDistane && Vector2.Distance(transform.position, player.position) > retreatDistane) {
            transform.position = this.transform.position;
        } else if (Vector2.Distance(transform.position, player.position) < retreatDistane){
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }

        if (timeBtwShots <= 0){
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        } else {
            timeBtwShots -= Time.deltaTime;
        }
    }
}
