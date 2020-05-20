using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Stalactite : MonoBehaviour
{
    Animator anim;
    float randomSpawnTime;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        randomSpawnTime = Random.Range(.5f, 4f);
        

    }
    private void Update()
    {
        randomSpawnTime -= Time.deltaTime;
        if (randomSpawnTime <= 0)
        {
            anim.SetTrigger("go");
        }

    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
