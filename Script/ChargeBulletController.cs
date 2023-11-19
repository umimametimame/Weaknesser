using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ChargeBulletController : BulletControll
{
    public float fadeSpeed;
    public int chargeLevel;
    public float[] levelPowRatio;
    public GameObject childObject;
    public SpriteRenderer childSprite;
    void Start()
    {
        ChangeOnChargeLevel();
        BulletInitialize();
        childObject = transform.GetChild(0).gameObject;
        childSprite = childObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        BulletMove();
        BulletOut(); 
        transform.localEulerAngles = new Vector3(0, 0, GetAim(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)));
        transform.localScale = Vector2.MoveTowards(transform.localScale, new Vector2(transform.localScale.x, 0.0f), fadeSpeed * Time.deltaTime);
        if (transform.localScale.y <= 0.0f) { BulletInvisible(); }
    }


    private void ChangeOnChargeLevel()
    {
        switch(chargeLevel)
        {
            case 0:
                childSprite.color = new Color(0.3f,0.9f,0.9f);
                pow.basic  *= levelPowRatio[0];
                break;
            case 1:
                childSprite.color = new Color(0.1f,0.6f,0.0f);
                pow.basic *= levelPowRatio[1];
                break;
            case 2:
                childSprite.color = new Color(0.8f, 0.5f, 0.0f);
                pow.basic *= levelPowRatio[2];
                break;
            case 3:
                childSprite.color = new Color(0.8f, 0.0f, 0.0f);
                pow.basic *= levelPowRatio[3];
                break;
        }
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