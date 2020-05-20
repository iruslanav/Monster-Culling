using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate : MonoBehaviour
{
    [SerializeField] GameObject staleactiteFall;
    public void on()
    {
        staleactiteFall.SetActive(true);
    }
    public void off()
    {

        staleactiteFall.SetActive(false);
    }

}
