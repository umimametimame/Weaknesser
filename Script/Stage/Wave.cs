using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    private float time;
    [System.Serializable] public struct SpawnChara
    {
        public GameObject chara;
        public NeedValueType needValueType;
        public float needValue;
        public bool spawned;
        public bool destroyed;
    }

    public enum NeedValueType
    {
        Time,
        DestroyCount,
        DestroyArrayNumber,
        Flag,
    }

    public SpawnChara[] spawnChara;
    [SerializeField] private int destroy;
    public bool clear;
    void Start()
    {
        clear = false;
        for(int i = 0; i < spawnChara.Length; ++i)
        {
            spawnChara[i].chara.SetActive(false);
            spawnChara[i].spawned = false;
        }
    }

    void Update()
    {
        for (int i = 0; i < spawnChara.Length; ++i)
        {
            DestroyCount(ref spawnChara[i]);
            SpawnJudge(ref spawnChara[i]);
        }

        time += Time.deltaTime;
        clear = (destroy >= spawnChara.Length) ? true : false;
    }

    void SpawnJudge(ref SpawnChara c)
    {
        switch (c.needValueType)
        {
            case NeedValueType.Time:
                SpawnByTime(ref c);
                break;
            case NeedValueType.DestroyCount:
                SpawnByDestroyCount(ref c);
                break;
            case NeedValueType.DestroyArrayNumber:
                SpawnByArrayNumber(ref c);
                break;
        }
    }

    void SpawnByTime(ref SpawnChara c)
    {
        if (c.needValue <= time && c.spawned == false)
        {
            Spawn(ref c);
        }
    }

    void SpawnByDestroyCount(ref SpawnChara c)
    {
        if(destroy >= c.needValue)
        {
            Spawn(ref c);
        }
    }

    void SpawnByArrayNumber(ref SpawnChara c)
    {
        if(spawnChara[(int)c.needValue].destroyed == true)
        {
            Spawn(ref c);
        }
    }

    void DestroyCount(ref SpawnChara c)
    {
        if(c.chara == null && c.destroyed == false)
        {
            destroy++;
            c.destroyed = true;
        }
    }

    void Spawn(ref SpawnChara c)
    {
        if(c.spawned == false)
        {
            c.spawned = true;
            c.chara.SetActive(true);
        }
    }
}
