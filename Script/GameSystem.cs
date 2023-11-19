using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
public class GameSystem : SingletonMonoBehaviour<GameSystem>
{

    [SerializeField] private enum Controller
    {
        Non,
        KeybordMouse,
        Keybord,
        Pad,
    }

    [SerializeField] private Controller controller;



    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if(World.worldState == World.WorldState.InGame)
        {
            switch(controller)
            {
                case Controller.KeybordMouse:
                    break;
                case Controller.Keybord:
                    break;
                case Controller.Pad:
                    break;
            }
        }
    }
}
