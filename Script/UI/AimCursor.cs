using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class AimCursor : MonoBehaviour
{
    private Vector2 originalSize;
    [SerializeField] private float changeSizeRatio;
    [SerializeField] private float speed;
    [SerializeField] private Mouse mouse;
    void Start()
    {
        originalSize = transform.localScale;
        mouse = Mouse.current;
    }

    void Update()
    {
        transform.position = mouse.position.ReadValue();
        transform.Rotate(new Vector3(0, 0, 1));
        if (mouse.leftButton.isPressed)
        {
            transform.localScale = Vector2.MoveTowards(transform.localScale, originalSize * changeSizeRatio, speed * Time.deltaTime);
        }
        else 
        { 
            transform.localScale = Vector2.MoveTowards(transform.localScale, originalSize, speed * Time.deltaTime); 
        }
    }
}
