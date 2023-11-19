using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_CumMove : MotionData
{
    protected override void C_MotionExe()
    {
        attachChara.state = State.Move;
        base.C_MotionExe();
    }
    protected override void c_AddVector()
    {
        degree = GetAim(transform.position, World.player.transform.position);
        v.x = Mathf.Cos(degree * Mathf.PI / 180);
        v.y = Mathf.Sin(degree * Mathf.PI / 180);
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
