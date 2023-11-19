using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class E_BulletGenerator_00 : Generator
{
    private ParticleSystem particle;
    void Start()
    {
        GeneratorInitialize();
        particle = GetComponent<ParticleSystem>();
    }
    void Update()
    {
        GeneratorUpDate();

        if (trigger == true)
        {
            engine.bulletPrefab.transform.position = transform.position;
            engine.targetPos = World.player.transform.position;

            if (IsEven(generaterParameter.burst))
            {
                for (int i = 0; i < generaterParameter.burst;)
                {
                    float addDegree = generaterParameter.diffusion * (generaterParameter.burst / (i + 2));
                    generaterParameter.degree[i] = GetAim(transform.position, engine.targetPos) + addDegree;
                    generaterParameter.degree[i + 1] = GetAim(transform.position, engine.targetPos) - addDegree;
                    i += 2;
                }
            }
            else
            {
                for(int i = 0; i < generaterParameter.burst;)
                {
                    if (i == 0)
                    {
                        generaterParameter.degree[i] = GetAim(transform.position, engine.targetPos);
                        i++;
                    }
                    else
                    {
                        float addDegree = generaterParameter.diffusion * (generaterParameter.burst / (i + 2));
                        generaterParameter.degree[i] = GetAim(transform.position, engine.targetPos) + addDegree;
                        generaterParameter.degree[i + 1] = GetAim(transform.position, engine.targetPos) - addDegree;
                        i += 2;
                    }
                }
            }
            GenerateCondition();
        }
    }

}
