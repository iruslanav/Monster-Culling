
using System;
public class HealthSystemEnemies
{
    public event EventHandler OnHealthChanged;
    private float health;
    private float Maxhealth;
    public HealthSystemEnemies(float Maxhealth)
    {
        this.Maxhealth = Maxhealth;
        health = Maxhealth;
    }

    public float GetHealth()
    {
        return health;
    }
    public float GetHealthPercent()
    {
        return (health / Maxhealth);
    }
    public void Damage(float DamageAmount)
    {
        health -= DamageAmount;
        if (health < 0)
        {
            health = 0;
        }
        if (OnHealthChanged != null)
        {
            OnHealthChanged(this, EventArgs.Empty);
        }
    }
    public void Heal(float healAmount)
    {
        health += healAmount;
        if (health > Maxhealth)
        {
            health = Maxhealth;
        }
        if (OnHealthChanged != null)
        {
            OnHealthChanged(this, EventArgs.Empty);
        }
    }

}
