using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

class BossController_00 : Chara
{
    void Start()
    {
        CharaInitialize();
    }

    void Update()
    {
        CharaUpdate();
        switch (state)
        {
            case (State.Spawn):
                motions[0].C_Action();
                break;
            case (State.Idol):
                // Motion番号の抽選と実行
                if (turning == false) motionNumber = UnityEngine.Random.Range(1, 3);
                else motionNumber = UnityEngine.Random.Range(3, motions.Length - 1 );
                
                state = State.Move;
                if (hp.entity <= (hp.maxEntity / 1.8f) && turning == false)
                {
                    state = State.Event;
                }
                break;
            case (State.Move):
                motions[motionNumber].C_Action();
                break;
            case (State.Event):
                motions[motions.Length - 1].C_Action();
                break;
            case (State.Dead):
                Defeated();
                Destroy(gameObject);
                break;
        }

        Resolve();
        MoveLimit();
    }

    private void OnTriggerEnter2D(Collider2D you)
    {
        if (you.CompareTag("PlayerBullet"))
        {
            UnderAttack(you);
        }
    }

    private void OnTriggerStay2D(Collider2D you)
    {
        if (you.CompareTag("PlayerBullet"))
        {
            UnderAttack(you);
        }
    }
}
