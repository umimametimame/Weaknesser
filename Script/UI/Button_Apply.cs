using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Button_Apply : ButtonClass
{
    [SerializeField] private GameObject[] applyObj;
    public override void Start()
    {
        Initialize();
        for (int i = 0; i < applyObj.Length; i++)
        {
            if (applyObj[i].GetComponent<AddFunction>() != null)
            {
                button.onClick.AddListener(applyObj[i].GetComponent<AddFunction>().Apply);
            }
        }
    }
}
