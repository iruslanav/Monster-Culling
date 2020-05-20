using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XpBar : MonoBehaviour
{
	public Slider slider;
	public Image fill;
	public FloatValue currentXp;
	public FloatValue maxXp;
	public Button ultiButton;
	void Start()
	{
		SetMaxXp();
	}

	public void SetMaxXp()
	{
		slider.maxValue = maxXp.initialValue;
		slider.value = currentXp.RuntimeValue;
	}
	public void UpdateXp()
	{
		float xp = currentXp.RuntimeValue;
		if (slider.value + xp > xp)
		{
			slider.value = currentXp.RuntimeValue;
		}
		else
		{
			slider.value = xp;
		}
		CheckCharged();
	}
	public void CheckCharged()
	{
		if (currentXp.RuntimeValue >= maxXp.initialValue)
		{
			ultiButton.gameObject.SetActive(true);
		}
		else
		{
			ultiButton.gameObject.SetActive(false);
		}
	}
	public void SetXp(float xp)
	{
		slider.value = xp;
	}
}
