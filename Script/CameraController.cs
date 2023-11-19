using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;
    Chara playerStatus;

    public Moment quakeMoment;
    public Quake quake;
    void Start()
    {
        player = GameObject.Find("Player");
        playerStatus = player.GetComponent<Chara>();
        quakeMoment = new Moment(false, 0.1f);
        quake = new Quake(gameObject);
    }

    void Update()
    {

        if (playerStatus.underAttack)
        {
            quakeMoment.SetBool(true, true);
        }

    }
    private void FixedUpdate()
    {
        quakeMoment.ActionByTime(quake.Quaking);
        CameraQuake();
    }

    void CameraQuake()
    {
        //if (quake.runtime)
        //{
        //    Vector2 v;
        //    if (playerStatus.state == State.Dead)
        //    {
        //        if (quake.time < quake.doTime * 3)
        //        {
        //            float fadeCurve = (quake.doTime - quake.time);
        //            v.x = UnityEngine.Random.Range(fadeCurve, -fadeCurve);
        //            v.y = UnityEngine.Random.Range(fadeCurve, -fadeCurve);

        //            offset = v;
        //        }
        //        else
        //        {
        //            quake.time = 0.0f;
        //            offset = new Vector2(0, 0);
        //            quake.runtime = false;
        //        }
        //        quake.time += Time.deltaTime;

        //    }
        //    else
        //    {
        //        if (quake.time < quake.doTime)
        //        {
        //            {
        //                float fadeCurve = (quake.doTime - quake.time);
        //                v.x = UnityEngine.Random.Range(fadeCurve, -fadeCurve);
        //                v.y = UnityEngine.Random.Range(fadeCurve, -fadeCurve);

        //                offset = v;
        //            }
        //        }
        //        else
        //        {
        //            quake.time = 0.0f;
        //            offset = new Vector2(0, 0);
        //            quake.runtime = false;
        //        }
        //        quake.time += Time.deltaTime;
        //    }
        //}
    }
}
