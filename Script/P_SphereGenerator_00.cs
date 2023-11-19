using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SphereGenerator : MonoBehaviour
{
    private GameObject root;
    [SerializeField] private Slots_00 slots;

    [SerializeField] private GameObject spherePrefab;
    [SerializeField] private Level sphereLevel = new Level();
    private int childObjCount;
    private GameObject[] clone;
    private SphearController[] cloneScript;
    private float posRatio;
    private float spherePowRatio;




    public Level SphereLevel
    {
        get { return sphereLevel; }
    }
    public void SphereInitialize()
    {
        root = transform.root.gameObject;
        SphereAssemble();
    }

    public void SphereGeneraterUpdate()
    {

        childObjCount = transform.childCount;
        if (sphereLevel.now != childObjCount)
        {
            SphereAssemble();
        }

    }
    void SphereAssemble() // Sphearの位置と数を補正
    {
        for (int i = 0; i < childObjCount; ++i)
        { Destroy(clone[i].gameObject); }

        Array.Resize(ref clone, sphereLevel.now);
        Array.Resize(ref cloneScript, sphereLevel.now);
        posRatio = 2.0f / sphereLevel.now;
        spherePowRatio = (sphereLevel.now > 1) ? 0.7f : 1.0f; // sphearの個数が1より多いならpowを下げる

        for (int i = 0; i < sphereLevel.now; ++i)
        {
            clone[i] = Instantiate(spherePrefab);
            clone[i].transform.parent = transform.gameObject.transform;
            cloneScript[i] = clone[i].GetComponent<SphearController>();
            cloneScript[i].time = posRatio * i;
            cloneScript[i].generatorPowRatio = spherePowRatio;
        }
    }
}
class P_SphereGenerator_00 : SphereGenerator
{

    void Start()
    {
        SphereInitialize();
    }

    void Update()
    {
        SphereGeneraterUpdate();
    }
}
