using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Scriptable/Create GeneratorData")]
public class so_Generator : ScriptableObject
{
    public float time;
    public bool executed;
    public enum Control
    {
        Manual,
        Auto
    }
    public float rate;
    public int burst;
    public float diffusion;
    [System.NonSerialized] public float[] degree;
    public GameObject bulletPrefab;
    [System.NonSerialized] public GameObject root;
    [System.NonSerialized] public GameObject target;
    [System.NonSerialized] public Vector2 targetPos;
    [System.NonSerialized] public Vector2 offSet;
    public Control control;

}
