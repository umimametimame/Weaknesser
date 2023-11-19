using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable] public class Slot
{
    [field: SerializeField] public List<Generator> hunger { get; set; } = new List<Generator>();
    [field: SerializeField] public Generator inUse { get; set; }
    [field: SerializeField] public bool inUsing { get; set; }
    [field: SerializeField] public ValueChecker<int> hungerChecker { get; set; }
    public Slot() { }

    #region ÉvÉçÉpÉeÉB
    public Generator AddHunger
    {
        set { hunger.Add(value); }
    }
    #endregion
    public void Initialize()
    {
        hungerChecker = new ValueChecker<int>(hunger.Count);
        inUse = hunger[0];

    }
    public void Update()
    {
        inUse.trigger = inUsing;
    }
}

[Serializable] public class SlotsClass : AddFunction
{
    [field: SerializeField] protected List<Slot> slots { get; set; }  //SlotÇListÇ…äiî[
    protected virtual void Start()
    {
        TypeFinder type = gameObject.AddComponent<TypeFinder>();
        slots = type.GetAndInList<Slot>(GetType());

        Destroy(type);
    }
    protected virtual void Update()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].Update();
        }
    }
}

[Serializable] public class Slots_00 : SlotsClass
{
    [SerializeField] private Slot main1;    // |
    [SerializeField] private Slot main2;    // |
    [SerializeField] private Slot sub1;     // |
    [SerializeField] private Slot sub2;     // | êeÉNÉâÉXÇÃslotsÇ…äiî[


    protected override void Start()
    {
        base.Start();
        for(int i = 0;i < slots.Count; ++i)
        {
            slots[i].Initialize();
        }

    }
    protected override void Update()
    {
        base.Update();
    }
}
