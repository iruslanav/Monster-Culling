using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ZombieFox : Enemy
{
    private Rigidbody2D myRigidbody;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;

    public Animator animator;
    private Vector2 movementDirection;
    private float dirX;
    private float dirY;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    // Start is called before the first frame update
    void Start()
    {

        CreateHealthBar();
        currentState = EnemyState.idle;
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null)
        {
            CheckDistante();
            CheckAnimator();
        }
    }
    void CheckDistante()
    {
        
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && (Vector3.Distance(target.position, transform.position) > attackRadius))
        {
            if (currentState == EnemyState.idle  || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {

                animator.SetFloat("Horizontal", movementDirection.x);
                animator.SetFloat("Vertical", movementDirection.y);
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                myRigidbody.MovePosition(temp);
                ChangeState(EnemyState.walk);
                animator.SetBool("idle", false);
                animator.SetBool("moving", true);
            }
        }
        else
        {
            ChangeState(EnemyState.idle);
            animator.SetBool("moving", false);
            animator.SetBool("idle", true);
        }
    }
    public void CheckAnimator()
    {
        if (target.position.x < transform.position.x && target.position.y < transform.position.y)
        {
            dirX = -1;
            dirY = -1;
        }
        else if (target.position.x > transform.position.x && target.position.y > transform.position.y)
        {
            dirX = 1;
            dirY = 1;
        }
        else if (target.position.x < transform.position.x && target.position.y > transform.position.y)
        {
            dirX = -1;
            dirY = 1;
        }
        else if (target.position.x > transform.position.y && target.position.y < transform.position.y)
        {
            dirX = 1;
            dirY = -1;
        }
        movementDirection = new Vector2(dirX, dirY);
        movementDirection.Normalize();


        

    }

    private void ChangeState (EnemyState newState)
    {
        if (currentState!=newState)
        {
            currentState = newState;
        }
    }
}
