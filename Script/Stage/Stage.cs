using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public float time;
    public int nowWave;
    public bool turning;
    public Chara boss;
    public AudioSource bgm;
    public AudioClip[] bgmClip;
    public Moment bgmChange;
    [SerializeField] private UI_Static resultUI;
    [System.Serializable] public enum StageState
    {
        Start,
        WaveExe,
        WaveClear,
        BreakTime,
        StageClear
    }
    public StageState stageState;
    [System.Serializable] public struct WaveEngine
    {
        public Wave wave;
        public bool waveSurpassed;
        public float breakTime;
    }
    public WaveEngine[] waveEngine;
    private void Start()
    {
        time = 0.0f;
        nowWave = 0;
        turning = false;
        for (int i = 0; i < waveEngine.Length; ++i)
        {
            waveEngine[i].waveSurpassed = false;
            waveEngine[i].wave.gameObject.SetActive((i == 0) ? true : false);
        }
        bgm = GetComponent<AudioSource>();
        bgm.clip = bgmClip[0];
        bgm.Play();
    }

    private void Update()
    {
        switch (stageState)
        {
            case StageState.Start:
                waveEngine[nowWave].wave.gameObject.SetActive(true);
                stageState = StageState.WaveExe;
                break;
            case StageState.WaveExe:
                if (waveEngine[nowWave].wave.clear == true)
                {
                    time = 0.0f;
                    waveEngine[nowWave].waveSurpassed = true;
                    nowWave++;
                    stageState = (nowWave >= waveEngine.Length) ? StageState.StageClear : StageState.WaveClear; 
                }
                break;
            case StageState.WaveClear:
                if(time <= waveEngine[nowWave].breakTime)
                {
                    time += Time.deltaTime;
                    stageState = StageState.Start;
                }
                break;
            case StageState.StageClear:
                resultUI.InstantiateUI();
                break;
        }
        if(boss.state == State.Event && bgm.volume != 0 && turning == false)
        {
            bgm.volume -= 0.05f;
        }
        if(bgm.volume == 0)
        {
            StartCoroutine(C_BGMChange(0.5f));
            turning = true;
        }

    }

    public IEnumerator C_BGMChange(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        bgm.volume = 1.0f;
        bgm.clip = bgmClip[1];
        bgm.Play();
    }
}
