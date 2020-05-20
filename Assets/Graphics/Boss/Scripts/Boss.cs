using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Boss : Enemy
{
    public enum State
    {
        Start,
        Walk,
        Attack,
        Dash,
        Fly1,
        Rain,
        Fly2,
        Shoot,
        Fly3,
        Idle,
    }
    private enum StateDash
    {
        Charge,
        Attacking,
        Stop,
    }

    //dash
    private StateDash stateDash;
    public State current;
    private float chargeDelayStun = 0.2f;
    private float chargeDelayAttack = 2f;
    private Vector3 normalizedDirection;
    public float speedDash;
    private bool moving;


    [SerializeField] private SignalSender onStalactite;
    [SerializeField] private SignalSender offStalactite;



    private float timeFollow = 30;
    private float timeDash = 1;



    [SerializeField] private Transform posicionVolar1;
    [SerializeField] private Transform posicionVolar2;
    [SerializeField] private Transform posicionIdle;


    private Vector2 movementDirection;
    float dirX, dirY;
    public float attackRadius;

    private Rigidbody2D rb;
    Transform target;
    public Animator animator;
    public Animator animatorSlashes;
    private bool startBossBattle;
    private bool onIdle;
    private bool onDash;
    private bool onAttack;
    private bool onFly;
    private bool isActive;
    private bool isOver;
    private bool onWalk;


    void Awake()
    {
        isActive = false;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        CreateHealthBar();
        onAttack = false;
        onFly = false;
    }

    void Update()
    {
            switch (current)
            {

                case State.Start:
                current = State.Walk;
                    break;
                case State.Walk:
                    if (!isActive)
                    {
                        isActive = true;
                        FunctionTimer.Create(() => {
                            animatorSlashes.SetBool("attack", false);
                            animator.SetBool("attack", false);
                            animator.SetBool("walk", false);
                            animator.SetBool("dash", false);
                            animator.SetBool("fly", false);
                            animator.SetBool("idle", false);
                            stateDash = StateDash.Charge;
                            Debug.Log("Change to dash");
                            onAttack = false;
                            current = State.Dash;
                            isActive = false;
                        }, 30);
                    }
                    else
                    {
                        Follow();
                    }


                    break;

                case State.Dash:
                    if (!isActive)
                    {
                        isActive = true;
                        FunctionTimer.Create(() => {
                            animator.SetBool("attack", false);
                            animator.SetBool("walk", false);
                            animator.SetBool("idle", false);
                            animator.SetBool("dash", false);
                            animator.SetBool("fly", true);
                            current = State.Fly1;
                            isActive = false;
                        }, 15);
                    }
                    DashAttack();
                    break;

                case State.Fly1:
                    onStalactite.Raise();
                current = State.Rain;
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    gameObject.GetComponent<CapsuleCollider2D>().enabled = false;


                    break;
                case State.Rain:
                    if (!isActive)
                    {
                        isActive = true;
                        FunctionTimer.Create(() => {
                            animator.SetBool("attack", false);
                            animator.SetBool("walk", false);
                            animator.SetBool("idle", false);
                            animator.SetBool("dash", false);
                            animator.SetBool("fly", true);
                            offStalactite.Raise();
                            current = State.Idle;
                            isActive = false;
                        }, 15);
                    }
                    else
                    {
                        TakeOff();
                    }

                    break;
                case State.Fly2:
                current = State.Shoot;
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
                    break;

                case State.Shoot:
                    MoveShoot();

                    break;

                case State.Fly3:
                current = State.Idle;
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
                    break;
                case State.Idle:
                    if (!isActive)
                    {
                        isActive = true;
                        FunctionTimer.Create(() => {
                            animator.SetBool("attack", false);
                            animator.SetBool("walk", false);
                            animator.SetBool("dash", false);
                            animator.SetBool("fly", false);
                            animator.SetBool("idle", true);
                            gameObject.GetComponent<BoxCollider2D>().enabled = true;
                            gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
                            current = State.Walk;
                            isActive = false;
                        }, 15);
                    }
                    else
                    {
                        MoveIdle();
                    }

                    break;
            }
    }
    private void MoveIdle()
    {
        if (transform.position != posicionIdle.position)
        {
            MoveTo(posicionIdle.position, 3, 0);

        }
        else
        {
            if (gameObject.GetComponent<BoxCollider2D>().enabled == false &&
            gameObject.GetComponent<CapsuleCollider2D>().enabled == false)
            {
                SetIdleDirection();
                animator.SetBool("fly", false);
                animator.SetBool("idle", true);
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
                gameObject.GetComponent<CapsuleCollider2D>().enabled = true;

            }
        }
    }
    private void MoveShoot()
    {
        if (transform.position != posicionVolar2.position)
        {
            MoveTo(posicionVolar2.position, 3, 0);
        }
        else
        {

        }
    }
    private void TakeOff()
    {
        if (transform.position != posicionVolar1.position)
        {
            MoveTo(posicionVolar1.position, 7, 0);
        }
        else
        {
        }

       
    }
    public void MoveTo(Vector3 targetPosition, float Speed, float minimo)
    {
        if (Vector2.Distance(transform.position, targetPosition) > minimo)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);
        }

    }
    private void DashAttack()
    {

        switch (stateDash)
        {
            case StateDash.Charge:
                CheckAnimator2();
                animator.SetBool("dash", true);
                normalizedDirection = (target.position - transform.position).normalized;
                break;


            case StateDash.Attacking:

                
                chargeDelayAttack -= Time.deltaTime;

                if (chargeDelayAttack > 0)
                {
                    if (moving)
                    {

                        MoveToDirection2();
                    }
                    else
                    {
                        rb.velocity = Vector3.zero;
                        animator.SetBool("dash", false);
                        chargeDelayAttack = 2f;
                        stateDash = StateDash.Stop;
                        moving = true;
                    }
                }
                else
                {
                    rb.velocity = Vector3.zero;
                    animator.SetBool("dash", false);
                    chargeDelayAttack = 2f;
                    stateDash = StateDash.Stop;
                }

                break;

            case StateDash.Stop:
                chargeDelayStun -= Time.deltaTime;
                if (chargeDelayStun > 0)
                {

                    animator.SetBool("idle", true);

                }
                else
                {
                    animator.SetBool("idle", true);
                    chargeDelayStun = 1.0f;
                    stateDash = StateDash.Charge;
                }
                break;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("TileMapCollision"))
        {
            moving = false;
        }
    }
    private void SetIdleDirection()
    {
        animator.SetFloat("Horizontal", 0);
        animator.SetFloat("Vertical", -1);
    }
    public void SetOnPlay()
    {
        stateDash = StateDash.Attacking;
    }
        public void MoveToDirection2()
    {
        rb.velocity = normalizedDirection * speedDash * 5;
    }
    private void Follow()
    {

        if (Vector3.Distance(target.position, transform.position) >= attackRadius && current == State.Walk && onAttack)
        {

            CheckAnimator();
            current = State.Walk;
            animator.SetBool("walk", true);
            Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            rb.MovePosition(temp);
            onWalk = true;

        }
        else
        {
            DoAttack();

        }

    

    }

    private void DoAttack()
    {
            CheckAnimator();
            animator.SetBool("walk", false);
            animator.SetBool("attack", true);
            animatorSlashes.SetBool("attack", true);
        current = State.Attack;
            onWalk = false;
            onAttack = true;
            FunctionTimer.Create(() => {
                animator.SetBool("attack", false);
                animatorSlashes.SetBool("attack", false);
                if (onAttack)
                {
                    current = State.Walk;
                }
                else
                {

                    current = State.Dash;
                }

            }, 1.2f);

            
        
    }
    public void CheckAnimator2()
    {
        animator.SetFloat("Horizontal", movementDirection.x);
        animator.SetFloat("Vertical", movementDirection.y);
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
        else if (target.position.x > transform.position.x && target.position.y < transform.position.y)
        {
            dirX = 1;
            dirY = -1;
        }
        movementDirection = new Vector2(dirX, dirY);
        movementDirection.Normalize();




    }
    public void CheckAnimator()
    {
        animator.SetFloat("Horizontal", movementDirection.x);
        animator.SetFloat("Vertical", movementDirection.y);
        animatorSlashes.SetFloat("Horizontal", movementDirection.x);
        animatorSlashes.SetFloat("Vertical", movementDirection.y);

        Vector3 from = new Vector3(transform.position.x, transform.position.y);
        Vector3 to = new Vector3(target.position.x, target.position.y);
        float angle = CalculateAngle(from, to);

         if (angle < 30 && angle > 0.1)
        {
            dirX = 0;
            dirY = 1;
        }
        else if (angle < 60 && angle > 30)
        {
            dirX = -1;
            dirY = 1;
        }
        else if (angle < 120 && angle > 60)
        {
            dirX = -1;
            dirY = 0;
        }
        else if (angle < 150 && angle > 120)
        {
            dirX = -1;
            dirY = -1;
        }
        else if (angle < 210 && angle > 150)
        {
            dirX = 0;
            dirY = -1;
        }
        else if (angle < 240 && angle > 210)
        {
            dirX = 1;
            dirY = -1;
        }
        else if (angle < 300 && angle > 240)
        {
            dirX = 1;
            dirY = 0;
        }
        else if (angle < 330 && angle > 300)
        {
            dirX = 1;
            dirY = 1;
        }
        else if (angle < 359 && angle > 330)
        {
            dirX = 0;
            dirY = 1;
        }
        movementDirection = new Vector2(dirX, dirY);
        movementDirection.Normalize();
    }
    public static float CalculateAngle(Vector3 from, Vector3 to)
    {
        return Quaternion.FromToRotation(Vector3.up, to - from).eulerAngles.z;
    }
}
