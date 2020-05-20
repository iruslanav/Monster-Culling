using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;


public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}
public enum State
{
    Charge,
    Attacking,
    Stunned,
    stagger
}

public class Enemy : MonoBehaviour
{
   


    public EnemyState currentState;
    public string enemyName;
    public float moveSpeed;
    public FloatValue maxHealth;
    HealthSystemEnemies healthSystem;
    public HealthBarEnemy healthBar;
    public GameObject deathEffect;
    public LootTable thisLoot;
    public FloatValue currentXp;
    public FloatValue xpAmount;
    public SignalSender playerXpSignal;

    public GameObject hitEffect;
    public void CreateHealthBar()
    {
        healthSystem = new HealthSystemEnemies(maxHealth.initialValue);
        healthBar.Setup(healthSystem);
    }
    public void Heal(float healAmount)
    {
        healthSystem.Heal(healAmount);
    }
    private void TakeDamage(float damageAmount)
    {
        if (healthSystem != null)
        {
            healthSystem.Damage(damageAmount);
            Debug.Log("health: " + healthSystem.GetHealthPercent());
            if (healthSystem.GetHealth() <= 0)
            {
                SetXp();
                this.gameObject.SetActive(false);
                DeathEffect();
                FunctionTimer.Create(() => {
                    int ran = (int)Random.Range(1, 3);
                    for (int i = 0; i < ran; i++)
                    {
                        MakeLoot();
                    }
                }, 1.13f);
            }
        }
        
    }
    private void SetXp()
    {
        currentXp.RuntimeValue += xpAmount.initialValue;
        playerXpSignal.Raise();
        
    }
    private void DeathEffect()
    {
        if (deathEffect!=null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1.3f);
        }
    }
    private void MakeLoot()
    {

        if (thisLoot != null)
        {

                Drops current = thisLoot.LootProbability();
                if (current != null)
                {
                    Instantiate(current.gameObject, transform.position, Quaternion.identity);
                }
            
        }
    }
    private void HitEffect(Rigidbody2D myRigidbody)
    {
        if (hitEffect != null && myRigidbody.bodyType == RigidbodyType2D.Dynamic)
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.333f);
        }
    }
    public void Knock(Rigidbody2D myRigidbody, float knockTime, float damage)
    {
        StartCoroutine(KnockCo(myRigidbody, knockTime));
        HitEffect(myRigidbody);
        TakeDamage(damage);
    }

    private IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime)
    {
        if (myRigidbody != null && myRigidbody.bodyType == RigidbodyType2D.Dynamic)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = EnemyState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }
    public void KnockBoss(float damage)
    {
            TakeDamage(damage);
        Debug.Log("duele");
    }
}
