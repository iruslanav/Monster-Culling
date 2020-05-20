using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	public Slider slider;
	public Image fill;
	public FloatValue currentHealth;

	void Start() {
		SetMaxHealth();
	}

	public void SetMaxHealth()
	{
		slider.maxValue = currentHealth.initialValue;
		slider.value = currentHealth.RuntimeValue;
	}
	public void UpdateHealth()
	{
			float health = currentHealth.RuntimeValue;
			slider.value = health;
	}
	public void SetHealth(float health)
	{
		slider.value = health;
	}
}
