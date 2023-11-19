using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private string text;
    private void Start()
    {
        DontDestroyOnLoad(transform.parent);
        displayText = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        text = "worldState = " + World.worldState.ToString();
        text += "\ndifficultyLevel = " + World.Instance.difficultyLevel.ToString();
        text += "\ntimeScale = " + Time.timeScale.ToString();
        text += "\nplayerInShop = " + World.playerInShop;
        text += "\npause = " + World.pause;
        text += "\ntimeStop = " + World.timeStop;
        displayText.SetText(text);
    }
}
