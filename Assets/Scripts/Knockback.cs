using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Knockback : MonoBehaviour {

    public float thrust;
    public float knockTime;
    public FloatValue damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if (hit != null)
            {

                //ENEMIGO CHOCA CON ENEMIGO
                if (other.gameObject.CompareTag("Enemy") && other.isTrigger && gameObject.CompareTag("Enemy"))
                {
                    Vector2 difference = hit.transform.position - transform.position;
                    difference = difference.normalized * 1;
                    hit.AddForce(difference, ForceMode2D.Impulse);
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    other.GetComponent<Enemy>().Knock(hit, 0.2f, 0);
                }
                //JUGADOR GOLPEA ENEMIGO
                else if (other.gameObject.CompareTag("Enemy") && other.isTrigger && gameObject.CompareTag("Enemy") == false && gameObject.CompareTag("Projectile") == false)
                {
                    Vector2 difference = hit.transform.position - transform.position;
                    difference = difference.normalized * thrust;
                    hit.AddForce(difference, ForceMode2D.Impulse);
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    other.GetComponent<Enemy>().Knock(hit, knockTime, damage.RuntimeValue);
                }
                //ENEMIGO GOLPEA JUGADOR
                else if (other.gameObject.CompareTag("Player") && other.isTrigger && gameObject.CompareTag("Enemy") || gameObject.CompareTag("Projectile"))
                {
                    if (other.GetComponent<AndroidPlayer>().currentState != AndroidPlayer.PlayerState.stagger)
                    {
                        Vector2 difference = hit.transform.position - transform.position;
                        difference = difference.normalized * thrust;
                        hit.AddForce(difference, ForceMode2D.Impulse);
                        hit.GetComponent<AndroidPlayer>().currentState = AndroidPlayer.PlayerState.stagger;
                        other.GetComponent<AndroidPlayer>().Knock(knockTime, damage.RuntimeValue, false);
                    }
                }
                else if (other.gameObject.CompareTag("Player") && other.isTrigger && gameObject.CompareTag("Boss"))
                {
                    if (other.GetComponent<AndroidPlayer>().currentState != AndroidPlayer.PlayerState.stagger)
                    {
                        Vector2 difference = hit.transform.position - transform.position;
                        difference = difference.normalized * thrust;
                        hit.AddForce(difference, ForceMode2D.Impulse);
                        hit.GetComponent<AndroidPlayer>().currentState = AndroidPlayer.PlayerState.stagger;
                        other.GetComponent<AndroidPlayer>().Knock(knockTime, damage.RuntimeValue, false);
                    }
                }
               
            }

            }
        }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.CompareTag("Player") && other.isTrigger && gameObject.CompareTag("Enemy"))
            {
                if (other.GetComponent<AndroidPlayer>().currentState != AndroidPlayer.PlayerState.stagger)
                {
                    BoxCollider2D coli = gameObject.GetComponentInParent<BoxCollider2D>();
                    coli.enabled = false;
                    FunctionTimer.Create(() => {
                        coli.enabled = true;
                    }, 0.4f);
                }
            }
        }
    }
}
