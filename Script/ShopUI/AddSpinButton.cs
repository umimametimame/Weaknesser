
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSpinButton : BuyButton
{
    void Start()
    {
        LevelUpDate();
        C_BuyButtonInitialize();
    }

    void Update()
    {
        LevelUpDate();
        displayPanelText.SetText((level.now == 0) ? ("Add\nFrontSpin") : ("Add\nBehindSpin"));
        C_BuyButtonUpdate();
    }
    protected override void BuyButtonOnClick()
    {
        if (canBuy)
        {
            C_Buy();
            World.playerScript.spinLevel.now++;
        }
    }
    void LevelUpDate()
    {
        level = World.playerScript.spinLevel;
    }
}
