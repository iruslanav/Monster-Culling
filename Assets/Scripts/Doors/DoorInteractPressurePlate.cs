using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractPressurePlate : MonoBehaviour
{
    public GameObject doorGameObject;
    private float timer;
    private Door doorAnims;
    void Awake()
    {
        doorAnims = doorGameObject.GetComponent<Door>();
    }
  
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (/*collider.GetComponent<AndroidPlayer>() != null ||*/ collider.CompareTag("PESA") == true )
        {
            doorAnims.DownDoor();
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (/*collider.GetComponent<AndroidPlayer>() != null ||*/ collider.CompareTag("PESA") == true)
        {
            doorAnims.UpDoor();
        }
    }
}
