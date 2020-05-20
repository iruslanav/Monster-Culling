using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
     
    public Vector3 GetRoamingPosition(Vector3 startingPosition)
    {
        return startingPosition + GetRandomDir() * Random.Range(1f,7f);
        /* CODIGO PARA MOVER DE MANERA RANDOM
        
        MoveTo(roamPosition, 1f ,1f);
        float reachedPositionDistance = 1f;
        if (Vector3.Distance(transform.position, roamPosition) < reachedPositionDistance)
        {
            roamPosition = GetRoamingPosition(startingPosition);

        }*/
    }

    /*El siguiente metodo devuelve un vector de direccion, al cual se le ha pasado la posicion del personaje y del enemigo, se comprueba si esta a la izquierda
     o la derecha del jugador y de manera aleatoria se genera un vector de direccion. Si el jugador esta a la izquierda del enmigo el metodo devolvera un vector
     hacia la derecha en eje X y el eje Y es aleatorio, en cambio si el enemigo se encuentra a la derecha del jugador entonces se desplazara la izquierda */
    public  static Vector3 DirRetreat(Vector3 player, Vector3 enemy)
    {
        if (player.x > enemy.x && player.y > enemy.y)
        {
            return new Vector3(-1, UnityEngine.Random.Range(-1f, 0f)).normalized;
        }
        else if (player.x > enemy.x && player.y < enemy.y)
        {
            return new Vector3(-1, UnityEngine.Random.Range(0f, 1f)).normalized;
        }
        else if (player.x < enemy.x && player.y > enemy.y)
        {
            return new Vector3(1, UnityEngine.Random.Range(-1f, 0f)).normalized;
        }
        else if (player.x < enemy.x && player.y < enemy.y)
        {
            return new Vector3(1, UnityEngine.Random.Range(0f, 1f)).normalized;
        }
        else
        {
            return new Vector3(1, UnityEngine.Random.Range(0f, 1f)).normalized;
        }


    }
    /*El siguiente metodo devuelve un vector direccion totalmente aleatorio*/
    public static Vector3 GetRandomDir()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }

    /*A este metodo se le pasan una posicion de destino, un parametro de velocidad, y un paramatro que sirve de limitador. Si la distancia entre el objetivo es mayor al limite pasado
     anteriormente, entonces se mueve el enemigo en direccion del objetivo, a la velocidad del paso de parametros.*/
    public void MoveTo(Vector3 targetPosition, float Speed,float minimo)
    {
        if (Vector2.Distance(transform.position, targetPosition) > minimo)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);
        }

    }
    /*El siguiente metodo mueve al enemigo en una direccion sin tener en cuenta un maximo de distancia recorrida. se mueve en una direccion de manera indefinida. En este metodo se usa 
     un transform*/
    public void MoveToDirection(Vector3 normalizedDirection, float speed)
    {
        transform.position += normalizedDirection * speed * Time.deltaTime;
    }
    /*El siguiente metodo mueve al enemigo en una direccion sin tener en cuenta un maximo de distancia recorrida. se mueve en una direccion de manera indefinida. En este metodo se usa 
     un Rigidbody*/
    public void MoveToDirection2(Vector3 normalizedDirection, float speed, Rigidbody2D rb)
    {
        rb.velocity = normalizedDirection * 5 * speed * Time.deltaTime;
    }

    /*El siguiente metodo gira las animaciones dependiendo de si miran a la derecha o izquierda*/
    public void CheckRightLeft(float target, float enemy)
    {
        if (target > enemy)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

}



