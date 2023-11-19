using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private string levelTypetext;
    [SerializeField] private string arrow;
    private TextMeshProUGUI text;
    private Level beforeLevel = new Level();

    public enum LevelType
    {
        Pow,
        HP,
        EN,
        Repair,
        Speed
    }

    public LevelType levelType;
    void Start()
    {
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        beforeLevel.now = -1;
        LevelUIUpdate();
        
    }

    void Update()
    {
        LevelUIUpdate();
    }

    void LevelUIUpdate()
    {
        switch (levelType)
        {
            case LevelType.Pow:
                levelTypetext = "P:";
                ArrowAssemble(World.playerScript.pow.level);
                break;
            case LevelType.HP:
                levelTypetext = "H:";
                ArrowAssemble(World.playerScript.hp.level);
                break;
            case LevelType.EN:
                levelTypetext = "E:";
                ArrowAssemble(World.playerScript.en.level);
                break;
            case LevelType.Repair:
                levelTypetext = "R:";
                ArrowAssemble(World.playerScript.recover.level);
                break;
            case LevelType.Speed:
                levelTypetext = "S:";
                ArrowAssemble(World.playerScript.speed.level);
                break;
        }

    }
    void ArrowAssemble(Level level)
    {
        if (level.now != beforeLevel.now)
        {
            arrow = "";
            for (int i = 0; i < level.now; ++i)
            {
                arrow += ">";
            }
            text.SetText(levelTypetext + arrow);
        }
        beforeLevel.now = level.now;
    }
}
