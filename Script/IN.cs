using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IN : Chara
{
    [SerializeField] private GameObject shopUI;
    [SerializeField] private float invincibleTime;
    [SerializeField] private float time;
    void Start()
    {
        CharaInitialize();
        time = 0.0f;
    }

    void Update()
    {
        CharaUpdate();
        time += Time.deltaTime;
        if (InRangeOfCamera == false && time >= invincibleTime)
        {
            Destroy(gameObject);
        }
        Move();
        Resolve();
    }

    void Move()
    {
        engine.moveForce += new Vector2(-speed.entity, 0.0f);
    }

    private void OnTriggerEnter2D (Collider2D you)
    {
        if (you.CompareTag("Player"))
        {
            World.playerInShop = true;
            GameObject clone = Instantiate(shopUI);
            
            Invoke("Defeated", 0.1f);

        }
    }
}
