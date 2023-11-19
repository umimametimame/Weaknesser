using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Generator_00 : Generator
{
    void Start()
    {
        GeneratorInitialize();
        engine.targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        generaterParameter.degree[0] = GetAim(transform.position, engine.targetPos);
    }
    void Update()
    {
        GeneratorUpDate();
        engine.targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (trigger == true && World.timeStop == false)
        {
            engine.bulletPrefab.transform.position = transform.position;

            generaterParameter.degree[0] = GetAim(transform.position, engine.targetPos);

            GenerateCondition();

        }
    }

}
