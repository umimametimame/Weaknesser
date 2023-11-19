using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_CenterMove : MotionData
{
    private Vector3 startPos;
    private Vector3 endPos;
    [SerializeField] private bool arrival;
    protected override void C_MotionExe()
    {
        attachChara.state = State.Move;
        base.C_MotionExe();
    }

    protected override void c_MotionStart()
    {
        startPos = attachChara.transform.position;
        endPos = new Vector2(0.0f, 1.0f);
        base.c_MotionStart();
    }

    protected override void c_MotionUpdate()
    {
        c_AddVector();
        base.c_MotionUpdate();
        if (attachChara.transform.position == endPos && arrival == false)
        {
            for (int i = 0; i < useGenerator.Length; ++i)
            {
                useGenerator[i].trigger = true;
            }
            arrival = true;
        }

    }

    protected override void c_AddVector()
    {
        attachChara.transform.position = Vector2.Lerp(startPos, endPos, acceleration.Evaluate(time));
        base.c_AddVector();
    }

    protected override void C_MotionPreparation()
    {

        for (int i = 0; i < useGenerator.Length; ++i)
        {
            useGenerator[i].trigger = false;
        }
        arrival = false;
        base.C_MotionPreparation();
    }
}