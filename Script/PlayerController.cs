using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using AIE2D;
using UnityEngine.EventSystems;
using System.Linq.Expressions;
using UnityEngine.InputSystem;

public class PlayerController : Chara
{
    [field: SerializeField] public KeyCode beforeKey { get; set; } = KeyCode.None;
    private float nowTime = 0.0f;
    [SerializeField] private float inputReception;
    [field: SerializeField] public Level spinLevel { get; set; }
    [field: SerializeField] public bool[] slotSelect { get; set; }


    private GameObject childGenerators;
    private StaticAfterImageEffect2DPlayer afterImage;  
    public bool AfterImage{
        get
        {
            return afterImage.enabled;
        }
        set
        {
            afterImage.enabled = value;
        }
    }// ↑プロパティ


    public Dictionary<string, Level> parameterDic = new Dictionary<string, Level>();

    [SerializeField] private PlayerInput input;
    [field: SerializeField] public KeyMap_KeybordMouse key { get; set; }
    [SerializeField] private KeyMap_PAD pad;
    [SerializeField] private Interval interval;
    protected virtual void Start()
    {

        CharaInitialize();
        engine.animator = gameObject.GetComponent<Animator>();
        afterImage = GetComponent<StaticAfterImageEffect2DPlayer>();
        afterImage.enabled = false;
        childGenerators = transform.Find("Generators").gameObject;


        DictionarySet();
        key.Initialize();
        pad.Initialize();
        if (!input.user.valid)
        {
            Debug.Log("アクティブなプレイヤーではありません");
        }
        foreach (var device in input.devices)
        {
            Debug.Log(device.name);
        }
        foreach (var device in InputSystem.devices)
        {
            Debug.Log(device.name);
        }

        interval.Initialize(0.0f);
    }

    protected virtual void Update()
    {
        CharaUpdate();
        if (World.timeStop == false)
        {

            // Spaceキーによる速さ調整
            if (Convert.ToBoolean(key.Shift))
            { speed.entity = speed.minEntity; }
            else
            { speed.entity = speed.maxEntity; }

            switch (state)
            {
                case State.Idol:
                    PlayerMoveInput();
                    PlayerSpinInput();
                    break;

                case State.Spin:
                    motions[0].C_Action();
                    break;
                case State.Dead:
                    Defeated();
                    underAttackAccept = false;
                    
                    break;
            }
            SlotSelectting();
            // 移動数値合算後にplayerを移動させる
            Resolve();

            // プレイヤーの移動制限範囲
            MoveLimit();

        }

        void PlayerMoveInput()
        {


            engine.animator.SetFloat("Input_F", key.Move.y);
            engine.moveForce += key.Move * speed.entity;
        }

        void PlayerSpinInput()
        {
            nowTime--;
            if(pad.Move.y != 0.0f)
            {

            }
            if (Input.anyKeyDown)
            {
                foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(code))
                    {
                        if (motions[0].canUse)
                        {
                            if (0 <= nowTime && beforeKey == code && beforeKey == KeyCode.S)
                            {
                                state = State.Spin;
                            }
                            else if (0 <= nowTime && beforeKey == code && beforeKey == KeyCode.W)
                            {
                                state = State.Spin;
                            }
                            else if (0 <= nowTime && beforeKey == code && beforeKey == KeyCode.D)
                            {
                                if(spinLevel.now >= 1) { state = State.Spin; }
                            }
                            else if (0 <= nowTime && beforeKey == code && beforeKey == KeyCode.A)
                            {
                                if (spinLevel.now >= 2) { state = State.Spin; }
                            }
                            else
                            {
                                beforeKey = code;
                                nowTime = inputReception;
                            }
                            break;
                        }
                    }
                }
            }
        }

   void SlotSelectting()
        {
            slotSelect[0] = Convert.ToBoolean(key.Attack1);
            slotSelect[1] = Convert.ToBoolean(key.Attack2);
            slotSelect[2] = Convert.ToBoolean(key.Sub1);
            slotSelect[3] = Convert.ToBoolean(key.Sub2);
            
            
        }

    }
    private void DictionarySet()
    {
        List<string> getNameList = GetNameOfType<Parameter>();
        List<Parameter> findParameterList = GetTypesInList<Parameter>();
        for (int i = 0; i < findParameterList.Count; i++)
        {
            parameterDic.Add(getNameList[i], findParameterList[i].level);
            //parameterDic[getNameList[i]].now++;
            //Debug.Log(getNameList[i]);
            //Debug.Log(parameterDic[getNameList[i]]);
        }

        getNameList = GetNameOfType<Level>();
        List<Level> findLevelList = GetTypesInList<Level>();
        for(int i = 0;i < findLevelList.Count; i++)
        {
            parameterDic.Add(getNameList[i], findLevelList[i]);
            //Debug.Log(getNameList[i]);
            //Debug.Log(parameterDic[getNameList[i]]);
        }

    }
    private void OnTriggerEnter2D(Collider2D you)
    {
        if(state != State.Dead && underAttackAccept == true)
        {
            switch (you.tag)
            {
                case ("Enemy"):
                    UnderAttack(you);
                    break;
                case ("EnemyBullet"):
                    UnderAttack(you);
                    break;

                case ("IN"):
                     
                    break;
            }
        }
    }
}
