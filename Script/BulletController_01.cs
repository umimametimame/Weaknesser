using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class BulletController_01 : BulletControll
{
    // Start is called before the first frame update
    void Start()
    {
        BulletInitialize();
        BulletParameterInitialize();
    }

    // Update is called once per frame
    void Update()
    {
        BulletMove();
        BulletOut();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        BulletEnter(collider, gameObject); ;
    }
}
