using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddButtonOnString : BuyButton
{
    public string findLevelName;
    void Start()
    {

        level = World.playerScript.parameterDic[findLevelName];
        C_BuyButtonInitialize();
    }

    void Update()
    {
        C_BuyButtonUpdate();
    }
    protected override void BuyButtonOnClick()
    {
        if (canBuy)
        {
            C_Buy();
            World.playerScript.parameterDic[findLevelName].now++;
        }
    }
}
