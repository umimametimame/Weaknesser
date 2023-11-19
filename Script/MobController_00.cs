using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobController_00 : Chara
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
                motions[motionNumber].C_Action();
                break;
            case (State.Idol):
                state = State.Move;
                break;
            case (State.Move):
                generator[0].trigger = true;
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
        {
            if (you.CompareTag("PlayerBullet"))
            {
                UnderAttack(you);
            }
        }
    }
}
