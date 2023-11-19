using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 自動で削除されるObjectにのみ使用
/// </summary>
[Serializable] public class Instancer
{
    public enum DisplayState
    {
        NotDisplayYet,
        Displaying,
        Death,
    }
    [field: SerializeField] public GameObject obj { get; set; }
    [field: SerializeField, NonEditable] public List<GameObject> clones { get; set; } = new List<GameObject>();
    [field: SerializeField, NonEditable] public DisplayState state { get; private set;}
    [SerializeField] private AudioClip instanceSound;
    [SerializeField] private GameObject parent;


    /// <summary>
    /// Initialize時に指定した場合、親子関係が作られる
    /// </summary>
    [field: SerializeField] public Instancer afterObj;

    public virtual void Initialize(Instancer afterObj = null, GameObject parent = null)
    {
        if(parent != null) { this.parent = parent; }
        state = DisplayState.NotDisplayYet;
        if (afterObj != null) { this.afterObj = afterObj; }
    }

    public virtual void Update()
    {
        foreach(GameObject clone in clones) 
        {
            if (clone != null) { state = DisplayState.Displaying; }// 一つでも表示中なら
            
        }

        switch (state)
        {
            case DisplayState.NotDisplayYet:
                break;

            case DisplayState.Displaying:
                clones.RemoveAll(value => value == null);
                if (clones.Count == 0) { state = DisplayState.Death; }
                break;

            case DisplayState.Death:
                break;
        }
    }
    public virtual void Instance()
    {
        if (instanceSound != null) { FrontCanvas.instance.source.PlayOneShot(instanceSound); }
        clones.Add(GameObject.Instantiate(obj));
    }
    public virtual void Instance(GameObject parent)
    {
        if (instanceSound != null) { FrontCanvas.instance.source.PlayOneShot(instanceSound); }
        clones.Add(GameObject.Instantiate(obj, parent.transform));
    }

    /// <summary>
    /// stateがNotDisplayYetの場合のみInstanceを行う
    /// </summary>
    /// <param name="parent"></param>
    public virtual void InstanceOneShot(GameObject parent = null)
    {
        if (state == DisplayState.NotDisplayYet)
        {
            if (parent == null) { Instance(); }
            else { Instance(parent); }
        }
    }

    /// <summary>
    /// clonesの最後を渡す
    /// </summary>
    public GameObject lastObj
    {
        get { return clones[clones.Count - 1]; }
    }

    public bool displaying
    {
        get
        {
            switch (state)
            {
                case DisplayState.NotDisplayYet:
                    return false;

                case DisplayState.Displaying:
                    return true;

                case DisplayState.Death:
                    return false;
            }

            return false;
        }
    }

}
