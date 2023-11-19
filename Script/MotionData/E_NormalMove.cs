using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_NormalMove : MotionData
{
    protected override void C_MotionExe()
    {
        attachChara.state = State.Move;
        base.C_MotionExe();
    }

    protected override void c_AddVector()
    {
        v.x = UnityEngine.Random.Range(transform.position.x * 0.5f, transform.position.x * -1.2f);
        v.y = UnityEngine.Random.Range(transform.position.y * 0.5f, transform.position.y * -1.2f);
    }

    protected override void c_MotionStart()
    {
        for (int i = 0; i < useGenerator.Length; ++i)
        {
            useGenerator[i].trigger = true;
        }
        base.c_MotionStart();
        c_AddVector();
    }

    protected override void c_MotionUpdate()
    {
        base.c_MotionUpdate();
        c_ChanceTime_MD();
    }

    protected override void C_MotionPreparation()
    {
        for (int i = 0; i < useGenerator.Length; ++i)
        {
            useGenerator[i].trigger = false;
        }
        base.C_MotionPreparation();
    }
}
