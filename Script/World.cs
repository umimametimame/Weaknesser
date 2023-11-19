using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using AIE2D;
using TMPro;
using UnityEngine.SceneManagement;
using My;

public class World : SingletonMonoBehaviour<World> {
    public enum WorldState
    {
        Title,
        InGame,
        PlayerDeth,
        GameOver
    }
    public enum DifficultyLevel
    {
        Easy,
        Normal,
        Hard,
        Chaotic,
    }
    public static GameObject player;
    public static PlayerController playerScript;
    public static SphereGenerator playerSphearGenerator;
    public static WorldState worldState;
    [SerializeField] private WorldState worldStateInspector;

    [field: SerializeField] public DifficultyLevel difficultyLevel { get; set; }

    [SerializeField] private UI_Static debugUI = new UI_Static();       // |
    [SerializeField] private UI_Static playerUI = new UI_Static();      // |
    [SerializeField] private UI_Static pauseUI = new UI_Static();       // |
    [SerializeField] private UI_Static gameOverUI = new UI_Static();    // ↓
    private List<UI_Static> staticUIs = new List<UI_Static>();          // Listにまとめる
    
    public static bool playerInShop;
    public static bool pause;
    public static bool timeStop;
    public static bool hitBoxScan;
    public static int score;
    public static float stageTime;
    public static float overlapInterval;
    public static bool overlap;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += SceneChangeInitialize;
        staticUIs = GetTypesInList<UI_Static>();
        
        worldState = worldStateInspector;
        switch(worldState){
            case WorldState.Title:
                break;
            case WorldState.InGame:
                InGameInitialize();
                break;
        }
    }

    void Update()
    {
        DebugUI();
        for(int i = 0; i < staticUIs.Count; ++i)
        {
            staticUIs[i].UIUpdate();
        }
        switch (worldState)
        {
            case WorldState.Title:
                
                break;

            case WorldState.InGame:
            case WorldState.PlayerDeth:
            case WorldState.GameOver:
                InGameUpDate();
                break;
        }
    }

    void WorldStateChange()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "TitleScene":
                worldState = WorldState.Title;
                break;
        }
    }

    void SceneChangeInitialize(Scene nextScene, LoadSceneMode mode)
    {
        if (nextScene.name == "GameScene")
        {
            InGameInitialize();

        }
    }void InGameInitialize()
    {
            player = GameObject.Find("Player");
            playerScript = player.GetComponent<PlayerController>();
            playerUI.InstantiateUI();
            
            playerSphearGenerator = player.transform.Find("Spheres").GetComponent<SphereGenerator>();

            playerInShop = false;
            pause = false;
            timeStop = false;
            hitBoxScan = false;
            worldState = WorldState.InGame;
            score = 0;
            stageTime = 0.0f;
            overlap = false;
            overlapInterval = 0.0f;
    }

    public void InGameUpDate()
    { 
        if(worldState != WorldState.PlayerDeth &&  worldState != WorldState.GameOver)
        {
            Pause();
            Time.timeScale = 1.0f;
            // C_PlayerDeathHitStopのtimeScale変更を防止
        }
        Time.timeScale = (timeStop == true) ? 0.0f : Time.timeScale;
        timeStop = (pause == true || playerInShop == true) ? true : false;
        OverlapMeasures();

        PlayerDeathJudge();

        stageTime += Time.deltaTime;
        hitBoxScan =  Convert.ToBoolean(playerScript.key.Shift);
    }
    public void ShopINExit()
    {
        Time.timeScale = 1.0f;
    }

    /// <summary>
    /// プレイヤデス時のヒットストップ
    /// </summary>
    /// <returns></returns>
    public IEnumerator PlayerDeathHitStop()
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSecondsRealtime(0.3f);
        Time.timeScale = 0.3f;
        yield return new WaitForSecondsRealtime(2.0f);
        gameOverUI.InstantiateUI();
        Time.timeScale = 1;
    }

    public void PlayerDeathJudge()
    { 
        if (playerScript.state == State.Dead && worldState != WorldState.GameOver)
        { worldState = WorldState.PlayerDeth; }

        switch (worldState)
        {
            case WorldState.PlayerDeth:
                StartCoroutine(PlayerDeathHitStop());
                worldState = WorldState.GameOver;
                break;
            case WorldState.GameOver:
                break;
        }
    }

    public void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseUI.InstantiateUI("PauseUI");
            pause = true;
        }
    }

    public void DebugUI()
    {

        if (Input.GetKeyDown(KeyCode.F12))
        {
            if (debugUI.Displaying == false)
            {
                debugUI.InstantiateUI();
            }
            else
            {
                debugUI.CloseUI();
            }
        }
    }

    /// <summary>
    /// 弾発射時の処理の重複を対策する
    /// </summary>
    public void OverlapMeasures()
    {
        overlapInterval += Time.deltaTime;
        overlap = (overlapInterval >= 0.01f) ? false : true;

    }
}


public enum DifficultyLevel
{
    Easy,
    Normal,
    Hard,
    Chaotic,
}


/// <summary>
/// Unity StartにInitializeを書く<br/>
/// ButtonOnClickに処理を書く
/// </summary>


/// <summary>
/// Unity UpDate内にCanUseの判断処理を書くこと
/// MotionExeは以下を実行する<br/>{<br/>
/// 開始時:MotionStart<br/>
/// 実行中:MotionUpDate<br/>
/// 終了時:MotionPreparation<br/>}<br/>
/// 任意のタイミング:AddVector<br/>
/// 任意のタイミング:MotionCostPay
/// </summary>
public class MotionData : AddFunction
{
    public bool canUse;
    public bool runtime;
    public bool overrideByEvent;
    public AnimationCurve acceleration;
    public float doTime;
    public float time;
    public float chanceRatio;
    public float degree;
    public Chara attachChara;
    public Generator[] useGenerator;
    [NonSerialized] public Vector2 v;

    public Action action;
    public float costValue;

    public void C_Action()
    {
        C_MotionExe();
    }

    /// <summary>
    /// ステートの移行を追記してから実行
    /// </summary>
    protected virtual void C_MotionExe()
    {
        // state = State.AnyState;
        time += Time.deltaTime;
        if (runtime == false)
        {
            c_MotionStart();
            runtime = true;
        }

        c_MotionUpdate();

        attachChara.engine.moveForce += v.normalized * acceleration.Evaluate(time) * attachChara.speed.entity;
        if (time >= doTime)
        {
            C_MotionPreparation();
        }
    }

    /// <summary>
    /// moveForceを計算する処理
    /// </summary>
    protected virtual void c_AddVector() { }
    /// <summary>
    /// MotionExe開始時に一度だけ行う処理
    /// </summary>
    protected virtual void c_MotionStart() { }

    /// <summary>
    /// 開始後から終了まで行う処理
    /// </summary>
    protected virtual void c_MotionUpdate() 
    { 
        if(overrideByEvent == true)
        {
            if(attachChara.state == State.Event)
            {
                C_MotionPreparation();
            }
        }
    }

    /// <summary>
    /// 終了時に各変数を初期化<br/>
    /// time = 0.0f<br/>
    /// runtime = false<br/>
    /// state = State.Idol<br/>
    /// v = (0,0)<br/>
    /// </summary>
    protected virtual void C_MotionPreparation()
    {
        time = 0.0f;
        runtime = false;
        attachChara.state = State.Idol;
        v = new Vector2(0, 0);
    }


    /// <summary>
    /// モーションの弾を撃たない時間
    /// </summary>
    /// <returns></returns>
    public void c_ChanceTime_MD()
    {
        bool chance;
        chance = time > (doTime * chanceRatio);

        if(chance == true)
        {
            for (int i = 0; i < useGenerator.Length; ++i)
            {
                useGenerator[i].trigger = false;
            }
        }
        else
        {
            for (int i = 0; i < useGenerator.Length; ++i)
            {
                useGenerator[i].trigger = true;
            }
        }
    }

    /// <summary>
    /// 任意のタイミングで実行:costValueの値分減らす処理
    /// </summary>
    protected virtual void c_MotionCostPay() { }
}

