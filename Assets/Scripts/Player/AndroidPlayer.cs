using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityStandardAssets.CrossPlatformInput;
using CodeMonkey.Utils;
using UnityEngine.SceneManagement;

public class AndroidPlayer : PlayerManager
{
    public enum PlayerState
    {
        idle,
        walk,
        attack,
        stagger,
        roll,
        Ulti
    }
    public PlayerState currentState;
    private Vector2 movementDirection;
    private Vector2 CurrentMovementDirection;
    public float movementSpeed;
    public float MOVEMENT_BASE_SPEED = 5.0f;
    public float velocidadRodando = 2.0f;
    public Rigidbody2D rb;
    public Animator animator;
    public Animator animator2;
    float dirX, dirY;
    public GameObject death;


    //private bool isAtacking;
    //private bool isRolling;
    
    private bool AttackDisabled = true;
    private bool rollDisabled = true;
    private bool pause = false;

    void Start()
    {
        //CreateHealthBar();
        AttackDisabled = true;
        rollDisabled = true;
    }

    void Update()
    {
      
            if (!pause)
            {
                MoveJoystick();
            }
            Inputs();
        
       

    }
    void MoveJoystick()
    {
        if (currentState != PlayerState.attack && currentState != PlayerState.roll && currentState != PlayerState.stagger)
        {
                dirX = Mathf.RoundToInt(CrossPlatformInputManager.GetAxis("Horizontal"));
                dirY = Mathf.RoundToInt(CrossPlatformInputManager.GetAxis("Vertical"));
                movementDirection = new Vector2(dirX, dirY);
                movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
                movementDirection.Normalize();
                currentState = PlayerState.walk;
                rb.velocity = movementDirection * movementSpeed * MOVEMENT_BASE_SPEED;
        }

    }
    void Inputs()
    {


        if (movementDirection != Vector2.zero && currentState != PlayerState.stagger && currentState != PlayerState.Ulti)
        {
                    animator.SetFloat("Horizontal", movementDirection.x);
                    animator.SetFloat("Vertical", movementDirection.y);
                    animator2.SetFloat("Horizontal", movementDirection.x);
                    animator2.SetFloat("Vertical", movementDirection.y);
                    animator.SetBool("moving", true);
        }
        else if (movementDirection == Vector2.zero && currentState != PlayerState.stagger && currentState != PlayerState.Ulti)
        {
            animator.SetBool("moving", false);
            currentState = PlayerState.idle;
        }

        if (movementDirection == Vector2.zero && currentState != PlayerState.attack && currentState != PlayerState.stagger && currentState != PlayerState.Ulti)
        {
            animator.SetBool("standing", true);
        }
        else
        {
            animator.SetBool("standing", false);
        }

    }
    public void Rodar()
    {
        if (rollDisabled && currentState != PlayerState.stagger && currentState != PlayerState.attack && currentState != PlayerState.Ulti)
        {

            currentState = PlayerState.roll;
            AttackDisabled = false;
            CurrentMovementDirection = movementDirection;
            StartCoroutine(RollCo());
        }
    }
    public void TirarUlti()
    {
        float xp = 0;
        currentXp.RuntimeValue = xp;
        playerXpSignal.Raise();
        Debug.Log("UltiTirada");
        currentState = PlayerState.Ulti;
        animator.SetBool("Ulti", true);
        animator2.SetBool("Ulti", true);
        rollDisabled = false;
        AttackDisabled = false;
        FunctionTimer.Create(() => {
            animator.SetBool("Ulti", false);
            animator2.SetBool("Ulti", false);
            rollDisabled = true;
            AttackDisabled = true;
        }, 5);
    }
    public void Ataque()
    {
        if (AttackDisabled && currentState != PlayerState.stagger && currentState != PlayerState.roll && currentState != PlayerState.Ulti)
        {
            currentState = PlayerState.attack;
            rollDisabled = false;
            CurrentMovementDirection = movementDirection;
            StartCoroutine(AttackCo());
        }
    }

    private IEnumerator RollCo()
    {
        rb.velocity = CurrentMovementDirection * movementSpeed * MOVEMENT_BASE_SPEED * velocidadRodando;
        animator.SetBool("rolling", true);
        yield return new WaitForSeconds(.400f);
        animator.SetBool("rolling", false);
        AttackDisabled = true;
        rollDisabled = true;
        currentState = PlayerState.idle;

    }
    private IEnumerator AttackCo()
    {
        rb.velocity = Vector2.zero;
        animator.SetBool("attacking", true);
        animator2.SetBool("attacking", true);
        yield return new WaitForSeconds(.542f);
        animator.SetBool("attacking", false);
        animator2.SetBool("attacking", false);
        AttackDisabled = true;
        rollDisabled = true;
        currentState = PlayerState.idle;
    }
    public void Knock(float knockTime, float damage, bool stay)
    {

        //TakeDamage(damage);
        currentHealth.RuntimeValue -= damage;
        playerHealthSignal.Raise();
        if (currentHealth.RuntimeValue > 0.0)
        {
            if (!stay)
            {
                StartCoroutine(KnockCo(knockTime));
            }
        }
        else
        {

            gameObject.SetActive(false);
            GameObject effect = Instantiate(death, transform.position, Quaternion.identity);
            FunctionTimer.Create(() => {
                SceneManager.LoadScene("Menu");
            }, 1.5f);
        }
    }
    private IEnumerator KnockCo(float knockTime)
    {
        if (rb != null)
        {

            currentState = PlayerState.stagger;
            yield return new WaitForSeconds(knockTime);
            rb.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            rb.velocity = Vector2.zero;
        }
    }
}