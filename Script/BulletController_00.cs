using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class BulletController_00 : BulletControll
{
    void Start()
    {
        BulletInitialize();
    }

    void Update()
    {
        BulletMove();
        BulletOut();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        BulletEnter(collider, gameObject);
    }
}
