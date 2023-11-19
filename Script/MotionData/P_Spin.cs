using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Spin : MotionData
{
    [System.NonSerialized] public PlayerController p;
    [System.NonSerialized] public KeyCode key;
    private int angleX = 0;
    private int angleY = 0;

    private void Start()
    {
        p = attachChara.GetComponent<PlayerController>();
    }

    private void Update()
    {
        canUse = (p.en.entity >= costValue) ? true : false;
    }

    protected override void C_MotionExe()
    {
        attachChara.state = State.Spin;
        base.C_MotionExe();
    }

    protected override void c_AddVector()
    {
        if (angleX == 0) // X軸の移動を不能にする
        {
            //attachChara.engine.moveForce.x = 0;
            v.y += acceleration.Evaluate(time) * angleY * attachChara.speed.entity;
            
        }
        else if (angleY == 0) // Y軸の移動を不能にする
        {
            //attachChara.engine.moveForce.y = 0;
            v.x += acceleration.Evaluate(time) * angleX * attachChara.speed.entity;
        }
        base.c_AddVector();
        
    }

    protected override void c_MotionCostPay()
    {
        base.c_MotionCostPay();
        p.en.entity -= costValue;
    }

    protected override void c_MotionStart()
    {
        base.c_MotionStart();
        c_MotionCostPay();
        key = p.beforeKey;
    }

    protected override void c_MotionUpdate()
    {
        base.c_MotionUpdate(); 
        p.engine.animator.SetFloat("Input_F", 0.0f);
        if (key == KeyCode.S)
        {
            p.engine.animator.SetBool("UnderSpin", true);
            angleY = -1;
            angleX = 0;
        }
        else if (key == KeyCode.W)
        {
            p.engine.animator.SetBool("UpperSpin", true);
            angleY = 1;
            angleX = 0;
        }
        else if (key == KeyCode.D)
        {
            p.engine.animator.SetBool("UnderSpin", true);
            angleX = 1;
            angleY = 0;
        }
        else if (key == KeyCode.A)
        {
            p.engine.animator.SetBool("UpperSpin", true);
            angleX = -1;
            angleY = 0;
        }

        p.underAttackAccept = false;
        p.AfterImage = true;
        c_AddVector();
    }

    protected override void C_MotionPreparation()
    {
        base.C_MotionPreparation();
        attachChara.underAttackAccept = true;
        attachChara.engine.animator.SetBool("UnderSpin", false);
        attachChara.engine.animator.SetBool("UpperSpin", false);
        attachChara.engine.animator.SetFloat("Input_F", 1.0f);
        p.AfterImage = false;
    }
}
