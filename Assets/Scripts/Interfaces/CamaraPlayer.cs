using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraPlayer : MonoBehaviour
{
    private Transform Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").transform;
    }
    
    // Update is called once per frame
    void Update()
    {
        Vector3 PlayerPos = Player.position;
        PlayerPos.z = transform.position.z;
        transform.position = PlayerPos;
    }
}
