using My;
using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �G�t�F�N�g�pScript<br/>
/// Sprite���Q�Ƃ��ďՌ��g�����
/// </summary>
public class ShockWave : MonoBehaviour
{

    [SerializeField] private GameObject targetObj;  // SpriteRenderer�܂���Image���܂�GameObject
    [SerializeField] private SpriteOrImage img;
    [SerializeField] private ShockWaveParameter param;
    private float initialAlpha;
    [SerializeField] private float nowAlpha;
    [SerializeField] private Vector3 initialScale;
    [SerializeField] private Vector3 nowScale;
    [SerializeField] private Vector3 toWoardsScale;
    [SerializeField] private Vector3 scaleLavel;
    [SerializeField] private float time = 0.0f; // 0�ɏ�����

    /// <summary>
    /// ������SpriteRenderer�t���I�u�W�F�N�g<br/>
    /// �����x�̉��Z���x<br/>
    /// �g�U�̑��x<br/>
    /// ��������<br/>
    /// </summary>
    /// <param name="targetObj"></param>
    /// <param name="alphaChangeSpeed"></param>
    /// <param name="spreadSpeed"></param>
    public void Initialize(GameObject targetObj, float alphaChangeSpeed, float spreadSpeed, float lifeTime)
    {
        this.targetObj = targetObj;
        param.alphaChangeSpeed = alphaChangeSpeed;
        param.spreadLevel = spreadSpeed;
        param.lifeTime = lifeTime;
        
    }

    /// <summary>
    /// �����͓����x�̉��Z���x<br/>
    /// �g�U�̑��x<br/>
    /// ��������<br/>
    /// </summary>
    /// <param name="alphaChangeSpeed"></param>
    /// <param name="spreadSpeed"></param>
    public void Initialize(float alphaChangeSpeed, float spreadSpeed, float lifeTime)
    {
        param.alphaChangeSpeed = alphaChangeSpeed;
        param.spreadLevel = spreadSpeed;
        param.lifeTime = lifeTime;

    }

    public void Initialize(GameObject targetObj, ShockWaveParameter parameter)
    {
        this.targetObj = targetObj;
        param = parameter;
    }
    public void Initialize(ShockWaveParameter parameter)
    {
        param = parameter;
    }
    private void Start()
    {
        img = new SpriteOrImage();
        img.Initialize(gameObject);

        time = 0.0f;
        initialAlpha = img.color.a;
        initialScale = targetObj.transform.localScale;

        ArgumentState();
    }

    public void Update()
    {
        if (time > param.lifeTime) Destroy(gameObject);
        switch (param.deathMode)
        {
            case ShockWaveParameter.DeathMode.ReachMinAlpha:
                if (img.color.a <= 0.0f) Destroy(gameObject);
                break;
            case ShockWaveParameter.DeathMode.ReachMaxAlpha:
                if(img.color.a >= 1.0f) Destroy(gameObject);
                break;
            case ShockWaveParameter.DeathMode.ReachLifeTime:
                break;

        }

        Shocking();
    }

    /// <summary>
    /// ������State�̂悤�Ɉ���
    /// </summary>
    private void ArgumentState()
    {
        //switch (param.lifeTime)
        //{
        //    case 0:
        //        Debug.Log("Shock�N���X��param.lifeTime��0�ł�");
        //        break;
        //    case -1:
        //        deathByLifeTime = false;
        //        break;
        //}

        //switch (param.alphaChangeSpeed)
        //{

        //    case 0:

        //        break;
        //    case -1:

        //        alphaToWardsLifeTime = true;
        //        param.alphaChangeSpeed = (1.0f / param.lifeTime) / img.color.a;
        //        break;
        //}
        switch (param.deathMode)
        {
            case ShockWaveParameter.DeathMode.ReachMinAlpha:

                break;
            case ShockWaveParameter.DeathMode.ReachMaxAlpha:

                break;
            case ShockWaveParameter.DeathMode.ReachLifeTime:
                toWoardsScale = initialScale * param.spreadLevel;
                scaleLavel = toWoardsScale / param.lifeTime;
                nowScale = initialScale;
                if(param.alphaChangeSpeed < 0.0f)
                {
                    param.alphaChangeSpeed = -(1.0f / Mathf.Abs(img.color.a)) / param.lifeTime;

                }
                else
                {
                    param.alphaChangeSpeed = (1.0f / Mathf.Abs(img.color.a)) / param.lifeTime;

                }

                break;
        }

    }

    private void Shocking()
    {
        time += Time.deltaTime;
        
        
        if (param.deathMode == ShockWaveParameter.DeathMode.ReachLifeTime)
        {
            nowAlpha = param.alphaChangeSpeed * time;
            img.color = new Color(img.color.r, img.color.g, img.color.b, initialAlpha - nowAlpha);
            nowScale = new Vector3(initialScale.x * param.spreadLevel * time, initialScale.y * param.spreadLevel * time); // scale�Ɋɋ}������
            nowScale = Vector3.Lerp(initialScale, toWoardsScale, time / param.lifeTime);
            targetObj.transform.localScale = new Vector3(nowScale.x, nowScale.y, targetObj.transform.localScale.z);

        }
        else
        {
            nowAlpha = param.alphaChangeSpeed * time;
            img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a - nowAlpha);
            targetObj.transform.localScale = new Vector3(nowScale.x, nowScale.y, targetObj.transform.localScale.z);

        }

        toWoardsScale = initialScale * param.spreadLevel;
    }
}


[Serializable] public class ShockWaveInstancer : Instancer
{
    [SerializeField] private ShockWaveParameter parameter;
    private ShockWave clScript;


    public override void Instance()
    {
        base.Instance();
        GameObject clone = lastObj;
        clScript = clone.gameObject.AddComponent<ShockWave>();
        clScript.Initialize(clone, parameter);
    }
    public override void Instance(GameObject parent)
    {
        base.Instance(parent);
        GameObject clone = lastObj;
        clScript = clone.gameObject.AddComponent<ShockWave>();
        clScript.Initialize(clone, parameter);
    }


}

[Serializable] public class ShockWaveParameter
{
    public enum DeathMode
    {
        ReachMaxAlpha,
        ReachMinAlpha,
        ReachLifeTime,
    }

    [field: SerializeField] public DeathMode deathMode { get; set; }
    [field: SerializeField] public float alphaChangeSpeed { get; set; }
    [field: SerializeField] public float spreadLevel { get; set; }
    [field: SerializeField] public float lifeTime { get; set; }
}

