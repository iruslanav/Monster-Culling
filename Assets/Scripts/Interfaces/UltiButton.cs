using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltiButton : MonoBehaviour
{
    public SignalSender xpCharged;
    public void DoUlti()
    {
        xpCharged.Raise();
    }
}

