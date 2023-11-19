using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class Button_Instantiate : ButtonClass
{
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private GameObject obj;
    [SerializeField] private UI_Static uiS = new UI_Static();
    [SerializeField] private UI_Instant uiI = new UI_Instant();
    public override void Start() {
        Initialize();
    }

    public override void ButtonOnClick()
    {
        base.ButtonOnClick();
        GameObject objClone;
        if(obj != null){
            objClone = Instantiate(obj);
        }
        if(uiS.Obj != null){
            uiS.InstantiateUI();
        }
        if (uiI.Obj != null)
        {
            uiI.InstantiateUI();
        }
    }
}
