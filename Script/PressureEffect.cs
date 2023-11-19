using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureEffect : MonoBehaviour
{
    [SerializeField] private GameObject contraction;
    [SerializeField] private SpriteRenderer conSprite;
    [SerializeField] private Vector2 conScale;
    [SerializeField] private float conTime;
    [SerializeField] private AnimationCurve conTimeCurve;
    [SerializeField] private GameObject expansion;
    [SerializeField] private SpriteRenderer expSprite;
    [SerializeField] private Vector2 expScale;
    [SerializeField] private AnimationCurve expTimeCurve;
    [SerializeField] private float expTime;
    [SerializeField] private float time;
    [SerializeField] private AudioClip se;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private bool turning;

    private void Start()
    {
        conScale = contraction.transform.localScale;
        conSprite = contraction.GetComponent<SpriteRenderer>();
        conSprite.color = new Color(1.0f, 1.0f, 1.0f, conTimeCurve.Evaluate(time));
        expScale = expansion.transform.localScale;
        expSprite = expansion.GetComponent<SpriteRenderer>();
        expansion.transform.localScale = new Vector2(0.0f, 0.0f);
        expSprite.color = new Color(1.0f, 1.0f, 1.0f, 1.0f - expTimeCurve.Evaluate(time));
        audioSource = GetComponent<AudioSource>();
        turning = false;
    }

    private void Update()
    {
        C_Contracrion();
        C_Expansion();
        time += Time.deltaTime;
    }

    public void C_Contracrion()
    {

        if(time <= conTime && turning == false)
        {
            conSprite.color = new Color(1.0f, 1.0f, 1.0f, conTimeCurve.Evaluate(time));
            contraction.transform.localScale = Vector2.Lerp(conScale, new Vector2(0.0f, 0.0f), (time / conTime));
        }
        else if(time > conTime && turning == false)
        {
            Destroy(contraction);
            time = 0.0f;
            audioSource.PlayOneShot(se);
            turning = true;
        }
        

    }

    public void C_Expansion()
    {
        if (time <= expTime && turning == true)
        {
            expSprite.color = new Color(1.0f, 1.0f, 1.0f, 1.0f - expTimeCurve.Evaluate(time));
            expansion.transform.localScale = Vector2.Lerp(new Vector2(0.0f, 0.0f), expScale, (time / expTime));
        }
        else if(time > expTime && turning == true) Destroy(gameObject);
    }

}
