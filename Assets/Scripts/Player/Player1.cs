using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player1 : MonoBehaviour
{
    public Vector2 movementDirection;
    public Vector2 CurrentMovementDirection;
    public float movementSpeed;
    public float MOVEMENT_BASE_SPEED = 5.0f;
    public float velicidadRodando = 6.0f;
    public Rigidbody2D rb;
    public Animator animator;
    float dirX, dirY;


    private bool isAtacking;
    private float delayAttackTime = 0.2f;
    private float delayAttackCounter = .2f;

    private bool isRolling;
    private float delayRollTime = 1f;
    private float delayRollCounter = 1f;


    private bool RollPressed;
    private bool AttackPressed;
    private bool pause = false;
    void Start()
    {

    }

    // Update is called once per frame




    void Update()
    {

     
            if (!pause)
            {
                Move();
            }
            Inputs();
        
        

    }

    void Move()
    {

        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        movementDirection.Normalize();
        rb.velocity = movementDirection * movementSpeed * MOVEMENT_BASE_SPEED;
    }
    void Inputs()
    {
        /* si se esta moviendo entonces pasamos al animador la posicion del 
        personaje para que determine que animacion tomar dependiendo de la direccion*/


        if (movementDirection != Vector2.zero)
        {
            animator.SetFloat("Horizontal", movementDirection.x);
            animator.SetFloat("Vertical", movementDirection.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }


        /* el siguiente codigo sirve para cuando se deja de rodar,
        si no hay un movimiento, se pase a la animacion de estar quieto*/


        if (movementDirection == Vector2.zero && !isAtacking && !isRolling)
        {
            animator.SetBool("standing", true);
        }
        else
        {
            animator.SetBool("standing", false);
        }

        //--DELAYER--
        /*Este codigo ses encarga de dar delay*/

        if (RollPressed || AttackPressed)
        {
            if (RollPressed)
            {
                delayRollCounter -= Time.deltaTime;
                if (delayRollCounter <= 0)
                {
                    RollPressed = false;
                    delayRollCounter = delayRollTime;
                }
            }
            else if (AttackPressed)
            {
                delayAttackCounter -= Time.deltaTime;
                if (delayAttackCounter <= 0)
                {
                    AttackPressed = false;
                    delayAttackCounter = delayAttackTime;
                }
            }
        }

        /*--RODAR---
         Comenzando desde la segunda condicion, Si se pulsa el boton H, 
         entonces se guarda el valor de la direccion, y se comienza un coRoutine( PARA ENTENDER BUSCAR METODO RollCo() )
         en este se pone en falso isRolling y RollPressed de manera que cuando acabe la animacion las condiciones ya no
         funcionaran hasta que haya pasado el delay */
        if (isRolling && !RollPressed)
        {
            rb.velocity = CurrentMovementDirection * velicidadRodando;
            movementDirection.Normalize();
        }

        if (Input.GetKeyDown(KeyCode.H) && !RollPressed)
        {
            pause = true;
            CurrentMovementDirection = movementDirection;
            isRolling = true;
            StartCoroutine(RollCo());


        }
        if (isAtacking && !AttackPressed)
        {
            rb.velocity = Vector2.zero;
        }
        if (Input.GetKeyDown(KeyCode.J) && !AttackPressed)
        {
            pause = true;
            StartCoroutine(AttackCo());
            isAtacking = true;
        }



    }

    /* un coRoutine es un metodo a parte que se ejecuta a la vez del codigo principal. 
    dentro de este se le pasa al animador los valores para que comience la animacion, utilizando
    el metodo "waitforseconds" hacemos que el codigo se detenga durante un tiempo
    lo hacemos para que no se interrumpa el rodar. Una vez pasa el tiempo le dejamos
    saber al animador que se ha acabado la animacion. Tambien ponemos en false 
    "isRolling y RollPressed*/
    private IEnumerator RollCo()
    {
        animator.SetBool("rolling", true);
        yield return new WaitForSeconds(.400f);
        pause = false;
        animator.SetBool("rolling", false);
        isRolling = false;
        RollPressed = true;

    }
    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        yield return new WaitForSeconds(.5f);
        pause = false;
        animator.SetBool("attacking", false);
        isAtacking = false;
        AttackPressed = true;

    }


}