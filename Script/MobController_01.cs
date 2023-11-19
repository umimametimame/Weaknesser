using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobController_01 : Chara
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
            case State.Spawn:
                motions[0].C_Action();
                break;
            case State.Idol:
                state = State.Move;
                break;
            case State.Move:
                motions[1].C_Action();
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
}
