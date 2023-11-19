using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddENButton : BuyButton
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
            World.playerScript.en.level.now++;
            Debug.Log("Buy and Add EN");
        }
    }
    void LevelUpDate()
    {
        level = World.playerScript.en.level;
    }
}
