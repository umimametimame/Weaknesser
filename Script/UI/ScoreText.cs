using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    private TextMeshProUGUI displayText;
    private int visual = 0;
    public bool fin;
    void Start()
    {
        displayText = GetComponent<TextMeshProUGUI>();
        displayText.SetText("Score:" + World.score.ToString("d6"));
    }

    void Update()
    {
        if(fin == false)
        {
            displayText.SetText("Score:" + World.score.ToString("d6"));
        }else
        {
            visual = (visual < World.score) ? visual += 100 : World.score;
            displayText.SetText("Score:" + visual.ToString("d6"));
        }

    }
}
