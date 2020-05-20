using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarEnemy : MonoBehaviour
{
    private HealthSystemEnemies healthSystem;
    public void Setup(HealthSystemEnemies healthSystem)
    {
        this.healthSystem = healthSystem;

        healthSystem.OnHealthChanged += healthSystem_OnHealChanged;
    }

    private void healthSystem_OnHealChanged(object sender, EventArgs e)
    {
        transform.Find("Bar").localScale = new Vector3(healthSystem.GetHealthPercent(), 1);
    }

}
