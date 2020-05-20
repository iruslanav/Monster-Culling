using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthReaction : MonoBehaviour
{
    public FloatValue playerHealth;
    public SignalSender healthSignal;
    public float restar;
    
    public void Use(float amountToIncrease)
    {

        if (playerHealth.RuntimeValue + amountToIncrease >= playerHealth.initialValue)
        {
            restar = ((playerHealth.RuntimeValue + amountToIncrease) - playerHealth.initialValue);
            amountToIncrease -= restar;
        }
        playerHealth.RuntimeValue += amountToIncrease;
        healthSignal.Raise();
    }
}
