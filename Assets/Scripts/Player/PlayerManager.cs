using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public FloatValue currentHealth;
    public FloatValue currentXp;
    public SignalSender playerHealthSignal;
    public SignalSender playerXpSignal;

    // Start is called before the first frame update
    /*public void CreateHealthBar()
    {
        playerHealth = currentHealth.initialValue;
        //healthBar.SetMaxHealth(currentHealth.initialValue);
    }*/
    /*public void TakeDamage(float damage)
    {
        currentHealth.RuntimeValue -= damage;
        playerHealthSignal.Raise();
        // healthBar.SetHealth(playerHealth);
    }
    public void Heal(float heal)
    {
        currentHealth.RuntimeValue += heal;

        //healthBar.SetHealth(playerHealth);
    }*/

}
