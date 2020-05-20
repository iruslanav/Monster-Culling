using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Door entryDoor;
    [SerializeField] private Door exitDoor;
    [SerializeField] private BattleSystem battleSystem;

    private void Start()
    {
        battleSystem.OnBattleStarted += BattleSystem_OnBattleStarted;
        battleSystem.OnBattleOver += BattleSystem_OnBattleOver;
    }

    private void BattleSystem_OnBattleStarted(object sender, EventArgs e)
    {
        entryDoor.UpDoor();
        exitDoor.UpDoor();
    }
    private void BattleSystem_OnBattleOver(object sender, EventArgs e)
    {

        entryDoor.DownDoor();
        exitDoor.DownDoor();
    }
}
