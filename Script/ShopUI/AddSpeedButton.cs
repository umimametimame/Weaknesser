using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSpeedButton : BuyButton
{
    void Start()
    {
        LevelUpDate();
        C_BuyButtonInitialize();
    }

    void Update()
    {
        LevelUpDate();
        C_BuyButtonUpdate();
    }
    protected override void BuyButtonOnClick()
    {
        if (canBuy)
        {
            C_Buy();
            World.playerScript.speed.level.now++;
        }
    }
    void LevelUpDate()
    {
        level = World.playerScript.speed.level;
    }
}