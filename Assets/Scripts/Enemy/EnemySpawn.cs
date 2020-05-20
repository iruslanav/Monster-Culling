using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CodeMonkey.Utils;

public class EnemySpawn : MonoBehaviour
{

    public GameObject appearEffect;

    public bool IsAlive()
    {
        if (gameObject.activeInHierarchy)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Spawn()
    {
        gameObject.SetActive(true);
        transform.SetParent(null);

        Knockback knock = GetComponent<Knockback>();
        ZombieFox fox = GetComponent<ZombieFox>();
        Enemigo2 en2 = GetComponent<Enemigo2>();
        Bug bug = GetComponent<Bug>();
        Boss boss = GetComponent<Boss>();
        SpriteRenderer render = GetComponent<SpriteRenderer>();
        Rigidbody2D rig = GetComponent<Rigidbody2D>();
        GameObject child = transform.GetChild(0).gameObject;
        if (fox != null && render != null && knock !=null && rig != null && child != null)
        {

            knock.enabled = false;
            fox.enabled = false;
            render.enabled = false;
            rig.bodyType = RigidbodyType2D.Static;
            child.SetActive(false);
            
        } else if (bug != null && render != null && knock != null && rig != null && child != null)
        {

            knock.enabled = false;
            bug.enabled = false;
            render.enabled = false;
            rig.bodyType = RigidbodyType2D.Static;
            child.SetActive(false);

        }
        else if (boss != null && render != null && knock != null && rig != null && child != null)
        {

            knock.enabled = false;
            boss.enabled = false;
            render.enabled = false;
            rig.bodyType = RigidbodyType2D.Static;
            child.SetActive(false);

        }
        else if (en2 != null && render != null && knock != null && rig != null && child != null)
        {

            knock.enabled = false;
            en2.enabled = false;
            render.enabled = false;
            rig.bodyType = RigidbodyType2D.Static;
            child.SetActive(false);

        }
        FunctionTimer.Create(() => {
            if (rig != null) rig.bodyType = RigidbodyType2D.Dynamic;
            if (knock != null) knock.enabled = true;
            if (fox != null) fox.enabled = true;
            if (bug != null) bug.enabled = true;
            if (en2 != null) en2.enabled = true;
            if (boss != null) boss.enabled = true;
            if (render != null) render.enabled = true;
            if (child != null) child.SetActive(true);
        }, 0.7f);


        if (appearEffect != null)
        {
            GameObject effect = Instantiate(appearEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1.3f);
        }

    }
}
