using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddSphearButton : BuyButton
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
            World.playerSphearGenerator.SphereLevel.now++;
            Debug.Log("Buy and Add Sphear");
        }
    }
    void LevelUpDate()
    {
        level = World.playerSphearGenerator.SphereLevel;
    }
}
