using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drops : MonoBehaviour
{
    private Vector2 direction;

    private void Awake()
    {
        direction = Random.insideUnitCircle.normalized * 8;
        gameObject.GetComponent<Rigidbody2D>().AddForce(direction, ForceMode2D.Impulse);
    }
}
