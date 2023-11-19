using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scapegoat : MotionData
{
    public AnimationCurve moveX;
    public AnimationCurve moveY;
    void Update()
    {
        
    }

    protected override void C_MotionExe()
    {
        attachChara.state = State.Move;
        base.C_MotionExe();
    }

    protected override void c_MotionUpdate()
    {
        base.c_MotionUpdate();
        c_AddVector();
    }

    protected override void c_AddVector()
    {
        attachChara.engine.moveForce = new Vector2(moveX.Evaluate(time), moveY.Evaluate(time)) * attachChara.speed.entity * acceleration.Evaluate(time);
        
        base.c_AddVector();
    }

    protected override void C_MotionPreparation()
    {
        base.C_MotionPreparation();
    }
}
