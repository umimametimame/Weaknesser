using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGuage : MonoBehaviour
{
    private enum GuageType
    {
        HP,
        EN,
        Speed
    }
    [SerializeField] private GuageType guageType;
    private RectTransform meRect;
    public float ratio;
    public float playerParameter;
    public float maxParameter;
    public Chara chara;
    void Start()
    {
        meRect = GetComponent<RectTransform>();
        chara = World.playerScript.GetComponent<Chara>();
    }
    void Update()
    {
        switch (guageType)
        {
            case GuageType.HP:
                playerParameter = chara.hp.entity;
                maxParameter = chara.hp.maxEntity;
                break;
            case GuageType.EN:
                playerParameter = chara.en.entity;
                maxParameter = chara.en.maxEntity;
                break;
        }
        ratio = (playerParameter <= 0) ? 0 : playerParameter / maxParameter;
        meRect.localScale = new Vector2(ratio, meRect.localScale.y);

    }
}
