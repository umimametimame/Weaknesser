using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorCharge : Generator
{
    [SerializeField] private float chargeRatio;
    public float generateChargeMinRatio;
    public int chargedLevel;
    public float chargeSpeed;
    public float[] needChargeRatio;

    void Start()
    {
        GeneratorInitialize();
        engine.targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        generaterParameter.degree[0] = GetAim(transform.position, engine.targetPos);
        chargeRatio = 0.0f;
        chargedLevel = 0;
    }
    void Update()
    {
        GeneratorUpDate();
        C_Charge();
        engine.targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (trigger == true && World.timeStop == false)
        {
            engine.bulletPrefab.transform.position = transform.position;

            generaterParameter.degree[0] = GetAim(transform.position, engine.targetPos);
            
        }
    }

    public void C_Charge()
    {
        if (trigger == true)
        {
            chargeRatio = (chargeRatio >= 1.0f) ? 1.0f : chargeRatio + Time.deltaTime;
            C_ChargeLevelSet();
        }
        else
        {
            if (chargeRatio > generateChargeMinRatio)
            {
                C_ChargeGenerate();
                chargeRatio = 0.0f;
                chargedLevel = 0;
            }
            chargeRatio = (chargeRatio <= 0.0f) ? 0.0f : chargeRatio - 0.001f;

        }
    }

    public void C_ChargeLevelSet()
    {
        for (int i = 0; i < needChargeRatio.Length; ++i)
        {
            chargedLevel = (chargeRatio >= needChargeRatio[i]) ? i : chargedLevel;
        }
    }
    public void C_ChargeGenerate()
    {
        if (canGenerate)
        {
            Invoke("C_CostPay", 0.01f);
            if (World.overlap == false)
            {
                World.overlap = true;
                engine.audioSource.PlayOneShot(engine.audioClip[0]);
            }
            time = 0.0f;
            for (int i = 0; i < generaterParameter.degree.Length; i++)
            {
                GameObject clone = Instantiate(engine.bulletPrefab, new Vector2(transform.position.x + engine.offSet.x, transform.position.y + engine.offSet.y), Quaternion.Euler(0, 0, generaterParameter.degree[i]));
                ChargeBulletController cloneScript = clone.GetComponent<ChargeBulletController>();
                CloneTag(engine.root.gameObject, clone);
                cloneScript.pow.basic *= clonePowRatio;
                cloneScript.chargeLevel = chargedLevel;
                if (rootFollow == true) { clone.transform.SetParent(transform); }
            }
            chargeRatio = 0.0f;
        }
    }
}
