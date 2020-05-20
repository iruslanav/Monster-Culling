using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : Enemy
{
    private enum State
    {
        Charge,
        Attacking,
        Stunned,
        stagger
    }

    public Rigidbody2D rb;
    public float speed;
    Transform target;
    private Vector3 savedPostionPlayer;
    private Vector3 normalizedDirection;
    public Animator animator;
    private bool isRolling;

    private float chargeDelayCharge;
    private float chargeDelayAttack = 1f;
    private float chargeDelayStun = 2.0f;
    private bool moving;

    private State state;
    // Start is called before the first frame update
    void Awake()
    {
        state = State.Charge;
    }
    void Start()
    {
        CreateHealthBar();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        chargeDelayCharge = Random.Range(0.4f, 1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (state)
        {
            case State.Charge:

                chargeDelayCharge -= Time.deltaTime;
                if (chargeDelayCharge > 0)
                {
                    animator.SetBool("idle", true);
                }
                else
                {
                    animator.SetBool("idle", false);
                    normalizedDirection = (target.position - transform.position).normalized;
                    chargeDelayCharge = Random.Range(0.4f, 1f);
                    state = State.Attacking;

                    CheckRightLeft();

                }


                break;
            case State.Attacking:

                chargeDelayAttack -= Time.deltaTime;
                if (chargeDelayAttack > 0)
                {
                    if (moving)
                    {
                        MoveToDirection2();
                        animator.SetBool("attack", true);
                    }
                    else
                    {
                        rb.velocity = Vector3.zero;
                        animator.SetBool("attack", false);
                        chargeDelayAttack = 1f;
                        state = State.Stunned;
                        moving = true;
                    }

                }
                else
                {
                    rb.velocity = Vector3.zero;
                    animator.SetBool("attack", false);
                    chargeDelayAttack = 1f;
                    state = State.Stunned;
                }

                break;

            case State.Stunned:


                chargeDelayStun -= Time.deltaTime;
                if (chargeDelayStun > 0)
                {
                    animator.SetBool("stun", true);

                }
                else
                {
                    animator.SetBool("stun", false);
                    chargeDelayStun = 2.0f;
                    state = State.Charge;
                }
                break;

            case State.stagger:

                break;


        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            moving = false;
        }
        if (collision.CompareTag("Trees"))
        {

            moving = false;
        }
        if (collision.CompareTag("Enemy"))
        {

            moving = false;
        }
    }
    public void CheckRightLeft()
    {
        if (target.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    public void MoveToDirection2()
    {
        rb.velocity = normalizedDirection * speed;
    }
}
