using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using JetBrains.Annotations;
using System.Reflection;
using System.Linq;

[Serializable] public class Level
{
    public int now;
    public int max;
}

[Serializable] public class Moment
{
    [SerializeField] private bool flag;
    [SerializeField] private float time;
    [SerializeField] private float interval;

    public Moment()
    {
        flag = false;
        time = 0.0f;
    }
    public Moment(bool inBool, float inInterval)
    {
        flag = inBool;
        interval = inInterval;
        time = 0.0f;
    }

    public bool GetFlag()
    {
        return flag;
    }

    public void SetBool(bool inBool, bool timeOverride)
    {
        flag = inBool;
        if (timeOverride) time = 0.0f;
    }


    public void ActionByTime(Action<bool> action)
    {
        if (flag == true)
        {
            if (time >= interval)
            {
                action(true);
                flag = false;
                time = 0.0f;
            }
            else
            {
                action(false);
                time += Time.deltaTime;

            }
        }
    }
}

[Serializable] public class Interval
{
    [field: SerializeField] public bool achieved { get; private set; }
    [SerializeField] private float interval;
    [SerializeField] private float time;

    public void Initialize(float startTime)
    {
        time = startTime;
        
        achieved = (time >= interval);
    }
    public void Initialize()
    {
        time = 0.0f;

        achieved = (time >= interval);
    }

    public void Update()
    {
        achieved = (time >= interval);
        time += Time.deltaTime;
    }
}

[Serializable] public class Quake
{
    [SerializeField] private GameObject img;
    [SerializeField] private Vector3 originalPos;
    [SerializeField] private Vector3 offset;
    [SerializeField] private bool even;
    [SerializeField] private bool child;
    [SerializeField] private float quakeRange;

    /// <summary>
    /// 子要素にSpriteがあるCharaの場合
    /// </summary>
    /// <param name="inChara"></param>
    public Quake(Chara inChara)
    {
        img = inChara.engine.sprite;
        originalPos = img.transform.parent.position;
        child = true;

        Initialize();
    }
    /// <summary>
    /// 本体にSpriteがある場合
    /// </summary>
    /// <param name="obj"></param>
    public Quake(GameObject obj)
    {
        img = obj;
        originalPos = img.transform.position;
        child = false;

        Initialize();
    }

    public void Quaking(bool finalize)
    {
        OriginalPosSet();
        if (finalize == true)
        {
            img.transform.position = originalPos;
        }
        else
        {
            if (even == true)
            {
                offset.x = UnityEngine.Random.Range(quakeRange, -quakeRange);
                offset.y = UnityEngine.Random.Range(quakeRange, -quakeRange);
                img.transform.position += offset;
                even = false;
            }
            else
            {

                img.transform.position = originalPos;
                even = true;
            }

        }
    }

    private void OriginalPosSet()
    {
        if (child == true)
        {
            originalPos = img.transform.parent.position;
        }
        else
        {

        }
    }

    private void Initialize()
    {
        even = true;
        if(child == true)
        {
            offset = new Vector3(0, 0, 0);

        }
        else
        {
            offset = new Vector3(0, 0, -10);
        }
        quakeRange = 0.2f;

    }
}
[Serializable] public class ValueChecker<T>
{
    [SerializeField] private T beforeValue;
    [field: SerializeField] public bool changed { get; private set; }

    public ValueChecker(T value)
    {
        beforeValue = value;
    }

    public bool FlagCheck(T value) 
    {
        changed = !(value.Equals(beforeValue));
        beforeValue = value;
        Debug.Log(changed + " <- ValueChecker");
        return changed;
    }
    
}

public class AddFunction : MonoBehaviour
{

    private World.WorldState beforeState;
    private void Start()
    {
        beforeState = World.worldState;
    }
    public bool IsEven(int num)
    {
        return (num % 2 == 0);
    }
    public float GetAim(Vector2 p1, Vector2 p2)
    {
        Vector2 dt = p2 - p1;
        float rad = Mathf.Atan2(dt.x, dt.y);
        float degree = -(rad * Mathf.Rad2Deg) + 90;

        return degree;
    }

    public Vector2 DegToVec(float deg)
    {
        Vector2 v;

        v = new Vector2(Mathf.Cos(deg * Mathf.Deg2Rad), Mathf.Sin(deg * Mathf.Deg2Rad));

        return v;
    }

    /// <summary>
    /// UIが切り替わったら行われる処理
    /// </summary>
    public void C_UIFinalize(Action action)
    {
        if (World.worldState != beforeState)
        {
            action();
            beforeState = World.worldState;
        }
    }

    public List<string> GetNameOfType<T>()
    {
        Type targetType = typeof(T);
        FieldInfo[] fields = GetType().GetFields(
            BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.Instance
        );

        List<string> variableNames = new List<string>();

        foreach (var field in fields)
        {
            if (field.FieldType == targetType)
            {
                variableNames.Add(field.Name);
            }
        }

        return variableNames;
    }
    public List<T> GetTypesInList<T>()
    {
        FieldInfo[] fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

        List<T> variables = new List<T>();
        foreach (FieldInfo field in fields)
        {
            if (field.FieldType == typeof(T))
            {
                T variable = (T)field.GetValue(this);
                variables.Add(variable);
            }
        }
        return variables;
    }
    public IEnumerator DelayDestroy(GameObject obj, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Destroy(obj);
    }

    public virtual void Apply() { }
    public bool InRangeOfCamera
    {
        get
        {
            if (transform.position.x > 12 || -12 > transform.position.x || transform.position.y > 7 || -7 > transform.position.y)
            {
                return false;
            }
            return true;
        }
    }
}

/// <summary>
/// 指定した型を検索し、Listにして返す関数
/// </summary>
public class TypeFinder : MonoBehaviour
{
    [field: SerializeField] public FieldInfo[] fields { get; private set; }
    public List<T> GetAndInList<T>(Type type)
    {
        fields = type.GetFields(BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        List<T> variables = new List<T>();
        foreach (FieldInfo field in fields)
        {
            if (field.FieldType == typeof(T))
            {
                T variable = (T)field.GetValue(GetComponent(type));
                variables.Add(variable);

            }
        }

        return variables;
    }
}

