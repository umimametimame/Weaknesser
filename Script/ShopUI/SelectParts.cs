using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectParts : World
{
    private bool select;
    public GameObject buyPanel;
    void Start()
    {
        
    }

    void Update()
    {
        if (select)
        {
            buyPanel.SetActive(true);
        }
    }
}
