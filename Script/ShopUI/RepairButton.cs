using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairButton : BuyButton
{
    void Start()
    {
        C_BuyButtonInitialize();
        cost = baseCost;
    }

    void Update()
    {
        canBuy = World.playerScript.hp.entity != World.playerScript.hp.maxEntity && World.score >= cost ? true : false;
        if (canBuy == false)
        {
            displayPanelText.SetText("SoldOut!");
        }
        else displayPanelText.SetText("Repair");
        displayButtonText.SetText(cost.ToString());
    }
    protected override void BuyButtonOnClick()
    {
        if (canBuy)
        {
            C_Buy();
            World.playerScript.hp.entity = World.playerScript.hp.maxEntity;
            Debug.Log("Buy and Repair HP");
        }
    }
}
