using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private float saleValue;
    [SerializeField] private GameObject buyButtonsParent;
    [SerializeField] private BuyButton[] buyButtons;
    private void Start()
    {
        BaseCostSet();
    }

    public void BaseCostSet()
    {
        Array.Resize(ref buyButtons, buyButtonsParent.transform.childCount);
        for (int i = 0; i < buyButtonsParent.transform.childCount; ++i)
        {
            buyButtons[i] = buyButtonsParent.transform.GetChild(i).gameObject.GetComponent<BuyButton>();
            buyButtons[i].baseCost = (int)(buyButtons[i].baseCost * saleValue);
        }
    }
}
