using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelayButton : MonoBehaviour
{

    public float delayTime;
    public Button yourbutton;


    public void delay()
    {
        StartCoroutine(DelayPause());
    }

    private IEnumerator DelayPause()
    {
        yourbutton.interactable = false;
        yield return new WaitForSeconds(delayTime);
        yourbutton.interactable = true;
    }
}
