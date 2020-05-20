using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo2 : Enemy
{
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    private Transform player;
    private Rigidbody2D rb;
    private float timeBtwShots;
    public float startTimwBtwShots;
    public GameObject projectile;
    private Vector2 normalizedDirection;
    void Start()
    {

        CreateHealthBar();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {

        normalizedDirection = (player.position - transform.position).normalized;
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            rb.velocity = normalizedDirection * 5 * speed;

        }
        else if(Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            rb.velocity = Vector2.zero;
        }else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            rb.velocity = normalizedDirection * 5 * -speed;
        }

        if (timeBtwShots<= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = startTimwBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
        

    }

    

}
