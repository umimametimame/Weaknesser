using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

class E_BulletGenerator_01 : Generator
{
    void Start()
    {
        engine.target = World.player;
        GeneratorInitialize();
    }
    void Update()
    {
        GeneratorUpDate();

        if (trigger == true)
        {
            engine.bulletPrefab.transform.position = transform.position;
            engine.targetPos = engine.target.transform.position;

            if (IsEven(generaterParameter.burst))
            {
                for (int i = 0; i < generaterParameter.burst; i += 2)
                {               
                    generaterParameter.degree[i] = GetAim(transform.position, engine.targetPos) + generaterParameter.diffusion *((i + 2) / 2);
                    generaterParameter.degree[i + 1] = GetAim(transform.position, engine.targetPos) - generaterParameter.diffusion * ((i + 2) / 2);
                }
            }
            else
            {
                generaterParameter.degree[0] = GetAim(transform.position, engine.targetPos);
                for (int i = 1; i < generaterParameter.burst; i += 2)
                {
                    generaterParameter.degree[i] = GetAim(transform.position, engine.targetPos) + generaterParameter.diffusion * ((i + 2) / 2);
                    generaterParameter.degree[i + 1] = GetAim(transform.position, engine.targetPos) - generaterParameter.diffusion * ((i + 2) / 2);
                }
            }
            GenerateCondition();

        }
    }

}
