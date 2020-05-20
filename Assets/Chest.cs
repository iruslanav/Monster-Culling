using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{

    public LootTable thisLoot;
    public bool once = true;
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player") && once == true)
        {
            once = false;
            anim.SetTrigger("open");
            Drops current = thisLoot.LootProbability();
            Instantiate(current.gameObject, transform.position, Quaternion.identity);
            current = null;
        }
    }
}
