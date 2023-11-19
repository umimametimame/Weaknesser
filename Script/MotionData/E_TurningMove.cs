using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_TurningMove : MotionData
{
    private Vector3 startPos;
    private Vector3 endPos;
    [SerializeField] private GameObject pressureEffect;
    [SerializeField] private bool arrival;
    protected override void C_MotionExe()
    {
        attachChara.state = State.Event;
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
        attachChara.underAttackAccept = false;
        c_AddVector();
        base.c_MotionUpdate();
        if(attachChara.transform.position == endPos && arrival == false)
        {
            GameObject clone = Instantiate(pressureEffect);
            clone.transform.position = attachChara.transform.position;
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

        attachChara.underAttackAccept = true;
        attachChara.turning = true;
        base.C_MotionPreparation();
    }
}