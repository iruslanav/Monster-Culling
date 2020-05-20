using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator doorAnimator;
    public bool isUp;
    
    private void Awake()
    {

        doorAnimator = GetComponent<Animator>();
    }

    public void DownDoor()
    {
        isUp = false;
        Debug.Log("Down");
        doorAnimator.SetBool("Up", false);
    }

    public void UpDoor()
    {
        isUp = true;
        doorAnimator.SetBool("Up", true);
    }
    public void ToggleDoor()
    {
        isUp = !isUp;
        if (isUp)
        {
            UpDoor();
        }
        else
        {
            DownDoor();
        }
    }
    

}
