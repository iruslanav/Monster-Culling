using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject YouGameObject;

    private void OnTriggerEnter2D(Collision collision)
    {
        if (collision.gameObject == YouGameObject)
        {
            transform.position = new Vector3(36, -291, 0);
        }
    }
}
