using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private Chara chara;
    private TextMeshProUGUI me;
    void Start()
    {
        chara = World.player.GetComponent<Chara>();
        me = GameObject.Find("PlayerHP").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        me.text = chara.hp.entity < 0 ? "0" : ((int)chara.hp.entity).ToString();
        
    }
}
