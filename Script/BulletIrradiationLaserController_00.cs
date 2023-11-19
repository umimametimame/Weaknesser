using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class BulletIrradiationLaserController_00 : BulletControll
{
    public float fadeSpeed;
    void Start()
    {
        BulletInitialize();
    }

    void Update()
    {
        BulletMove();
        transform.localEulerAngles = new Vector3(0, 0, GetAim(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)));
        transform.localScale = Vector2.MoveTowards(transform.localScale, new Vector2(transform.localScale.x, 0.0f), fadeSpeed * Time.deltaTime);
        if (transform.localScale.y <= 0.0f) { BulletInvisible(); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BulletEnter(collision, gameObject);

    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        BulletEnter(collider, gameObject);
    }
}