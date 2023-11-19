using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using System.Reactive.Concurrency;

[Serializable] public class KeyMapClass
{
    public KeyMap keyMap { get; set; }
    [field:SerializeField] public PlayerInput input { get; private set; }

    public KeyMapClass()
    {
    }

    public void Initialize()
    {
        keyMap = new KeyMap();
        keyMap.Enable();
    }

    public KeyMap.KeybordMouseActions K
    {
        get
        {
            return keyMap.KeybordMouse;
        }
    }
    public KeyMap.PadActions P
    {
        get
        {
            return keyMap.Pad;
        }
    }
    public KeyMap.JoyStickActions J
    {
        get
        {
            return keyMap.JoyStick;
        }
    }
    public KeyMap.ShareActions S
    {
        get
        {
            return keyMap.Share;
        }
    }

    public virtual Vector2 Move { 
        get { 

            switch (input.currentControlScheme)
            {
                case "Keybord":
                    return K.Move.ReadValue<Vector2>().normalized;
                case "Pad":
                    return P.Move.ReadValue<Vector2>();
                case "JoyStick":
                    return J.Move.ReadValue<Vector2>();
            }
            return S.Move.ReadValue<Vector2>();
        } 
    }

    public virtual float Attack1 { 
        get
        {
            switch (input.currentControlScheme)
            {
                case "Keybord":
                    return K.Attack1.ReadValue<float>();
                case "Pad":
                    return P.Attack1.ReadValue<float>();
                case "JoyStick":
                    return J.Attack1.ReadValue<float>();
            }
            return S.Attack1.ReadValue<float>();
        } 
    }

    public virtual float Attack2
    {
        get
        {
            switch (input.currentControlScheme)
            {
                case "Keybord":
                    return K.Attack2.ReadValue<float>();
                case "Pad":
                    return P.Attack2.ReadValue<float>();
                case "JoyStick":
                    return J.Attack2.ReadValue<float>();
            }
            return S.Attack2.ReadValue<float>();
        }
    }

    public virtual float Esc { get { return 1.0f; } }
}

[Serializable] public class KeyMap_KeybordMouse : KeyMapClass
{
    public KeyMap_KeybordMouse() { }

    public override Vector2 Move { get { return K.Move.ReadValue<Vector2>().normalized; } }
    public override float Attack1 { get { return keyMap.KeybordMouse.Attack1.ReadValue<float>(); } }
    public override float Attack2 { get { return keyMap.KeybordMouse.Attack2.ReadValue<float>(); } }
    public float Sub1 { get { return (Attack1 > 0.0f && Shift > 0.0f) ? 1.0f : 0.0f; } }
    public float Sub2 { get { return (Sub1 > 0.0f && Shift > 0.0f) ? 1.0f : 0.0f; } }
    public float Shift { get { return keyMap.KeybordMouse.Shift.ReadValue<float>(); } }
    public override float Esc { get { return keyMap.KeybordMouse.Pause.ReadValue<float>(); } }

}
[Serializable]
public class KeyMap_PAD : KeyMapClass
{
    public KeyMap_PAD() { }

    public override Vector2 Move { get { return P.Move.ReadValue<Vector2>(); } }
    public override float Attack1 { get { return P.Attack1.ReadValue<float>(); } }
    public override float Attack2 { get { return P.Attack2.ReadValue<float>(); } }
    public float Sub1 { get { return P.Sub1.ReadValue<float>(); } }
    public float Sub2 { get { return P.Sub2.ReadValue<float>(); } }
    public float Shift { get { return P.Shift.ReadValue<float>(); } }
    public override float Esc { get { return P.Pause.ReadValue<float>(); } }

}

[Serializable] public class KeyMap_Joy : KeyMapClass
{
    public KeyMap_Joy() { }

    public override Vector2 Move { get { return J.Move.ReadValue<Vector2>(); } }
    public override float Attack1 { get { return J.Attack1.ReadValue<float>(); } }
    public override float Attack2 { get { return J.Attack2.ReadValue<float>(); } }
    public float Sub1 { get { return J.Sub1.ReadValue<float>(); } }
    public float Sub2 { get { return J.Sub2.ReadValue<float>(); } }
    public float Shift { get { return J.Shift.ReadValue<float>(); } }
    public override float Esc { get { return J.Pause.ReadValue<float>(); } }

}