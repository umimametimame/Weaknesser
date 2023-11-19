using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Spawn : MotionData
{
    protected override void C_MotionExe()
    {
        base.C_MotionExe();
    }

    protected override void c_MotionStart()
    {
        base.c_MotionStart();
        v = DegToVec(degree);
    }

    protected override void C_MotionPreparation()
    {
        base.C_MotionPreparation();
    }
}
