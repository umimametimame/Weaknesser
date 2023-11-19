using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public enum State
{
    Spawn,
    Idol,
    Move,
    Shield,
    Spin,
    Event,
    Hit,
    Dead
}
[Serializable] public class Parameter
{
    public float entity;
    public float basic;
    public float maxEntity;
    public float minEntity;
    public Level level;
    public float adjust;
    public float incremental;
}

[Serializable] public class BaseParameter
{

}
public class Status : AddFunction
{

    public State state;

    public void ParameterInitialize(ref Parameter p)
    {
        p.incremental = p.adjust * p.level.now;
        p.entity = p.maxEntity = p.basic + p.incremental;
        p.minEntity = p.maxEntity * 0.5f;
    }
    public void ParameterUpDate(ref Parameter p)
    {
        p.incremental = p.adjust * p.level.now;
        p.maxEntity = p.basic + p.incremental;
        p.minEntity = p.maxEntity * 0.5f;
        p.entity = (p.maxEntity <= p.entity) ? p.maxEntity : p.entity;
    }
}

/// <summary>
/// キャラクターオブジェクトに付けます<br/>
///　開始時:Initialize、stateはSpawnになる<br/>
///　実行中:Updateに加え、各stateの処理を書く<br/>
///　終了時:破壊処理を書く
/// </summary>
public class Chara : Status
{
    public Parameter hp;
    public Parameter en;
    public Parameter pow;
    public Parameter speed;
    public Parameter recover;
    public void CharaParameterInitialize()
    {
        ParameterInitialize(ref pow);
        ParameterInitialize(ref hp); 
        ParameterInitialize(ref en);
        ParameterInitialize(ref speed);
        ParameterInitialize(ref recover);
    }

    public void CharaParameterSet()
    {
        en.entity += (recover.entity) * Time.deltaTime;

        ParameterUpDate(ref pow);
        ParameterUpDate(ref hp);
        ParameterUpDate(ref en);
        ParameterUpDate(ref speed);
        ParameterUpDate(ref recover);

    }

    public Moment quakeMoment;
    public Quake quake;
    public bool turning;
    [Serializable] public struct Engine
    {
        [NonSerialized] public GameObject sprite;
        [NonSerialized] public Animator animator;
        [NonSerialized] public Vector2 moveForce;
        [NonSerialized] public Rigidbody2D rb;

        [NonSerialized] public GameObject core;
        [NonSerialized] public CapsuleCollider2D coreCollider;
        [NonSerialized] public ParticleSystem coreParticle;

        [NonSerialized] public GameObject armore;
        public ParticleSystem burn;

        public AudioSource audioSource;
        public AudioClip[] audioClips;
    }
    public Engine engine;
    public Generator[] generator;
    public int motionNumber;
    public MotionData[] motions;
    public bool underAttackAccept; // 攻撃を受け付けるか 
    public bool underAttack;
    public bool deathRuntime;
    [NonSerialized] public DropItem dropItem;

    public void CharaInitialize()
    {
        ComponentSet();

        for (int i = 0; i < motions.Length; i++)
        {
            motions[i].time = 0.0f;
            motions[i].runtime = false;
        }

        underAttackAccept = true;
        underAttack = false;
        deathRuntime = false;
        turning = false;

        quakeMoment = new Moment(false, 0.1f);

        TagEnemyInitialize();
        CharaParameterInitialize();
    }

    private void ComponentSet()
    {
        engine.rb = GetComponent<Rigidbody2D>();

        for (int i = 0; i < transform.childCount; ++i)
        {
            GameObject child;
            child = transform.GetChild(i).gameObject;
            switch (child.name)
            {
                case ("Core"):
                    engine.core = child;
                    //engine.core.AddComponent<Chara>().parameter = gameObject.GetComponent<Status>().parameter;
                    engine.coreCollider = engine.core.GetComponent<CapsuleCollider2D>();
                    engine.coreParticle = engine.core.GetComponent<ParticleSystem>();
                    break;
                case ("Armore"):
                    engine.armore = child;
                    break;
                case ("Sprite"):
                    if (transform.CompareTag("Player") == true)
                    {
                        engine.sprite = transform.gameObject;
                    }
                    else
                    {
                        engine.sprite = child;
                        quake = new Quake(gameObject.GetComponent<Chara>());
                    }
                    engine.animator = engine.sprite.GetComponent<Animator>();
                    break;
                case ("Motions"):
                    Array.Resize(ref motions, child.transform.childCount);
                    for (int j = 0; j < child.transform.childCount; ++j)
                    {
                        motions[j] = child.transform.GetChild(j).GetComponent<MotionData>();
                    }
                    break;
                case ("DropItem"):
                    dropItem = child.GetComponent<DropItem>();
                    break;
                case ("Generator"):

                    break;
            }
        }
    }

    public void CharaUpdate()
    {
        StartCoroutine(UnderAttackInterval());
        if (engine.coreCollider != null)
        {
            engine.coreCollider.enabled = underAttackAccept;

        }
        UnityEngine.Random.InitState((int)(DateTime.Now.Millisecond));
        engine.moveForce = new Vector2(0.0f, 0.0f);

        if (hp.entity <= 0)
        {
            state = State.Dead;
        }

        // parameterの回復
        CharaParameterSet();
        //engine.sprite.color += new Color(0.0f, 0.0f, 0.0f, 0.3f);

        // WorldClassの処理
        if (World.hitBoxScan == true)
        {
            if (engine.core != null && engine.armore != null)
            {
                HitBoxScan();
                HitBoxRender(255, 255);
            }
        }
        else
        {
            if (engine.core != null && engine.armore != null)
            {
                HitBoxRender(255, 0);
                engine.armore.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                engine.coreParticle.Stop();
            }
        }
    }

    public void Resolve()
    {
        engine.rb.velocity = engine.moveForce;
    }

    public void MotionSet(Action[] m)
    {
        for (int i = 0; i < m.Length; ++i)
        {
            motions[i].action = m[i];

        }
    }

    public void FixedUpdate()
    {
        quakeMoment.ActionByTime(quake.Quaking);
    }
    /// <summary>
    /// motionの要素数から動作を抽選
    /// </summary>
    /// <returns></returns>
    public int MotionLottery()
    {
        int r = UnityEngine.Random.Range(1, (motions.Length));

        return r;
    }

    public bool ChanceTime(so_Motion m, float ratio)
    {
        bool exe;

        exe = m.time < (m.doTime * ratio);

        return exe;
    }

    /// <summary>
    /// 被弾時の処理
    /// </summary>
    /// <param name="co"></param>
    public void UnderAttack(Collider2D co)
    {
        underAttack = true;
        quakeMoment.SetBool(true, true);
    }


    public IEnumerator UnderAttackInterval()
    {
        if (underAttack == true)
        {
            yield return null;
            underAttack = false;
        }
        else
        {
            yield return null;
        }
    }

    /// <summary>
    /// engineに爆発エフェクトを入れる
    /// </summary>
    public void Defeated()
    {
        if (deathRuntime == false)
        {
            ParticleSystem clone = Instantiate(engine.burn);
            clone.transform.position = gameObject.transform.position;
            if (gameObject.CompareTag("Player"))
            {
                clone.transform.localScale = new Vector2(5, 5);
                SpriteRenderer meSprite = engine.sprite.GetComponent<SpriteRenderer>();
                meSprite.enabled = false;
            }
            else
            {
                dropItem.Drop();
                Destroy(gameObject);
            }
            clone.Play();
            deathRuntime = true;
        }
    }


    /// <summary>
    /// Tagが"Enemy"の場合に行われる初期化処理
    /// </summary>
    public void TagEnemyInitialize()
    {
        if (gameObject.CompareTag("Enemy"))
        {
            state = State.Spawn;
            motionNumber = 0;
            Parameter[] adjustParams = { hp, en, pow };
            SceneEditor_GameScene.globalPerDif.AdjustParameter(adjustParams);
            //hp.basic *= World.Instance.GetLeverageMagnification;
            //pow.basic *= World.Instance.GetLeverageMagnification;
        }
    }
    public void MoveLimit()
    {
        if (state != State.Spawn)
        {
            Vector2 limitPos = transform.position;
            limitPos.x = Mathf.Clamp(limitPos.x, -9, 9);
            limitPos.y = Mathf.Clamp(limitPos.y, -3.5f, 5);
            transform.position = limitPos;
        }
    }

    // HitBoxを可視化
    void HitBoxScan()
    {
        float time = 1.0f;
        time += Time.deltaTime;

        engine.armore.transform.Rotate(new Vector3(0.0f, 0.0f, -time));
        engine.coreParticle.Play();
    }

    void HitBoxRender(int c, int a)
    {
        engine.core.GetComponent<SpriteRenderer>().color = new Color(c, c, c, a);
        engine.armore.GetComponent<SpriteRenderer>().color = new Color(c, c, c, a);
    }
}

class BulletControll : Status
{
    public Parameter pow;
    public Parameter speed;
    public void BulletParameterInitialize()
    {
        ParameterInitialize(ref pow);
        ParameterInitialize(ref speed);
    }

    public float time;
    public bool penetrate;
    [System.Serializable]
    public struct Engine
    {
        public AnimationCurve acceleration;
        [System.NonSerialized] public Rigidbody2D rb;
        [System.NonSerialized] public Vector2 moveForce;
        [System.NonSerialized] public AudioSource audioSource;
        public AudioClip[] audioClips;
    }

    public Engine engine;
    public void BulletInitialize()
    {
        time = 0.0f;
        engine.rb = GetComponent<Rigidbody2D>();
        engine.audioSource = GetComponent<AudioSource>();
        BulletParameterInitialize();
    }

    // 画面外に出たら破棄
    public void BulletOut()
    {
        if (InRangeOfCamera == false)
        {
            BulletInvisible();
        }
    }
    public void BulletMove()
    {
        time += Time.deltaTime;
        engine.rb.velocity = transform.right * speed.entity * engine.acceleration.Evaluate(time);
    }

    public void BulletInvisible()
    {
        Destroy(gameObject);
    }
    public void BulletEnter(Collider2D you, GameObject me)
    {
        if (me.tag != you.tag)
        {
            switch (me.tag)
            {
                case ("PlayerBullet"):
                    BulletHit(you, ("Enemy"));
                    BulletHit(you, ("Obstacle"));
                    break;

                case ("EnemyBullet"):
                    BulletHit(you, ("Player"));
                    BulletHit(you, ("Obstacle"));
                    break;

                case ("Enemy"):
                    BulletHit(you, ("Player"));
                    BulletHit(you, ("Obstacle"));
                    break;

                default:
                    break;
            }

        }
    }
    public void BulletHit(Collider2D you, string tag)
    {
        if (you.CompareTag(tag))
        {
            Chara youRoot = you.transform.parent.GetComponent<Chara>();
            if (youRoot.underAttackAccept == true)
                //engine.audioSource.PlayOneShot(engine.audioClips[0]);
                youRoot.hp.entity -= pow.entity;
            if (penetrate == false)
            { BulletInvisible(); }
        }
    }
}
public class Generator : Status
{
    public Parameter rate;
    public Parameter pow;
    public Parameter speed;
    public void GeneratorParameterInitialize()
    {
        ParameterInitialize(ref rate);
        ParameterInitialize(ref pow);
        ParameterInitialize(ref speed);
    }

    public float time;
    [field: SerializeField] public bool trigger { get; set; }
    [field: SerializeField] public bool canGenerate { get; set; }
    public float clonePowRatio;
    public bool targeting;
    public bool triggerButtonOn;
    public bool triggerButtonUp;
    public float shotTimeOffset;

    public bool rootFollow;
    public enum Control
    {
        Manual,
        Auto
    }
    public enum SlotType
    {
        Main1,
        Main2,
        Sub1,
        Sub2
    }
    [System.Serializable]
    public struct GeneraterParameter
    {
        public float rate;
        public int burst;
        public float diffusion;
        [System.NonSerialized] public float[] degree;
        public float costEN;
    }

    [System.Serializable]
    public struct Engine
    {
        public GameObject bulletPrefab;
        [System.NonSerialized] public GameObject root;
        [System.NonSerialized] public Chara rootChara;
        [System.NonSerialized] public GameObject target;
        [System.NonSerialized] public Vector2 targetPos;
        public Vector2 offSet;
        [System.NonSerialized] public AudioSource audioSource;
        public AudioClip[] audioClip;
    }

    public Control control;
    public SlotType slotType;
    public GeneraterParameter generaterParameter;
    public Engine engine;

    public void GeneratorInitialize()
    {
        GeneratorParameterInitialize();
        RootTag();
        time = generaterParameter.rate - shotTimeOffset;
        clonePowRatio = engine.rootChara.pow.maxEntity;
        trigger = false;
        Array.Resize(ref generaterParameter.degree, generaterParameter.burst);
        engine.target = GameObject.Find("Player");
        engine.targetPos = engine.target.transform.position;
        engine.audioSource = GetComponent<AudioSource>();
    }

    public void GeneratorUpDate()
    {
        TriggerButtonSet();
        time += Time.deltaTime;
        if (engine.rootChara.state == State.Dead)
        {
            trigger = false;
        }
        clonePowRatio = pow.maxEntity * engine.rootChara.pow.maxEntity;
        canGenerate = (time >= generaterParameter.rate && engine.rootChara.en.entity >= generaterParameter.costEN) ? true : false;
        //canGenerate = (World.overlap == true && control == Control.Manual) ? true : canGenerate;
    }

    /// <summary>
    /// burstの数値分発射する
    /// </summary>
    public void Generate()
    {
        if (canGenerate)
        {
            Invoke("CostPay", 0.01f);
            if (World.overlap == false)
            {
                World.overlap = true;
                engine.audioSource.PlayOneShot(engine.audioClip[0]);
            }
            time = 0.0f;
            for (int i = 0; i < generaterParameter.degree.Length; i++)
            {
                GameObject clone = Instantiate(engine.bulletPrefab, new Vector2(transform.position.x + engine.offSet.x, transform.position.y + engine.offSet.y), Quaternion.Euler(0, 0, generaterParameter.degree[i]));
                CloneTag(engine.root.gameObject, clone);
                clone.GetComponent<BulletControll>().pow.basic *= clonePowRatio;
                if (rootFollow == true) { clone.transform.parent = transform; }
            }
        }
    }


    /// <summary>
    /// 発射条件
    /// </summary>
    public void GenerateCondition()
    {
        switch (control)
        {
            case Control.Manual:
                TriggerButtonSet();
                engine.targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (trigger)
                {
                    Generate();
                }
                break;
            case Control.Auto:
                Generate();
                break;
        }
    }
    public void CloneTag(GameObject root, GameObject clone)
    {
        switch (root.tag)
        {
            case ("Player"):
                clone.tag = ("PlayerBullet");
                break;
            case ("PlayerBullet"):
                clone.tag = ("PlayerBullet");
                break;
            case ("Enemy"):
                clone.tag = ("EnemyBullet");
                break;
            case ("EnemyBullet"):
                clone.tag = ("EnemyBullet");
                break;
            default:
                break;
        }
    }

    public void RootTag()
    {
        if (transform.parent.parent.CompareTag("Enemy"))
        {
            engine.root = transform.parent.parent.gameObject;
            engine.rootChara = engine.root.GetComponent<Chara>();
        }
        else
        {
            engine.root = transform.root.gameObject;
            engine.rootChara = engine.root.GetComponent<Chara>();
        }
    }

    public void TriggerButtonSet()
    {
        switch (slotType)
        {
            case SlotType.Main1:
                triggerButtonOn = Input.GetMouseButton(0);
                triggerButtonUp = Input.GetMouseButtonUp(0);
                break;
            case SlotType.Main2:
                triggerButtonOn = Input.GetMouseButton(0);
                triggerButtonUp = Input.GetMouseButtonUp(0);
                break;
            case SlotType.Sub1:
                triggerButtonOn = Input.GetMouseButton(1);
                triggerButtonUp = Input.GetMouseButtonUp(1);
                break;
            case SlotType.Sub2:
                triggerButtonOn = Input.GetMouseButton(1);
                triggerButtonUp = Input.GetMouseButtonUp(1);
                break;
        }
    }
    public void CostPay()
    {
        engine.rootChara.en.entity -= generaterParameter.costEN / World.playerSphearGenerator.SphereLevel.now;
        
    }
}

