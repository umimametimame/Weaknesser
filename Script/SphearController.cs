using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SphearController : MonoBehaviour
{
    private PlayerController root;
    [SerializeField] private Generator[] useGenerator;
    [SerializeField] private AnimationCurve spherMoveX;
    [SerializeField] private AnimationCurve spherMoveY;
    public float time { get; set; }
    public float generatorPowRatio { get; set; }
    void Start()
    {
        root = transform.root.GetComponent<PlayerController>();
        SphearAssemble();
    }

    void Update()
    {
        SphearMove(); 
        for (int i = 0; i < root.generator.Length; ++i)
        {
            int n = (int)useGenerator[i].slotType;
            useGenerator[i].trigger = root.slotSelect[n];
        }
    }
    void SphearMove()
    {
        time += Time.deltaTime;
        if (root.state != State.Dead)
        {
            transform.localPosition = new Vector2(spherMoveX.Evaluate(time) - 2, spherMoveY.Evaluate(time));
        }
    }

    void SphearAssemble()
    {
        Array.Resize(ref useGenerator, root.generator.Length);
        for (int i = 0; i < useGenerator.Length; ++i)
        {
            useGenerator[i] = Instantiate(root.generator[i]);
            useGenerator[i].transform.parent = transform;
            useGenerator[i].pow.basic *= generatorPowRatio;
        }
    }
}