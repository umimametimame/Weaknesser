using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public int dropScore;
    public GameObject[] dropItem;
    public bool droped;
    private void Start()
    {
        dropScore = (int)(dropScore * SceneEditor_GameScene.globalPerDif.adjustScore);
    }
    public void Drop()
    {
        if (droped == false)
        {
            World.score += dropScore;
            for (int i = 0; i < dropItem.Length; ++i)
            {
                Instantiate(dropItem[i]);
            }
        }
    }
}
