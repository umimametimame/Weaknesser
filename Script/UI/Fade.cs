using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : AddFunction
{
    public enum FadeState
    {
        None,
        FadeIn,
        FadeOut,
    }

    [SerializeField] private FadeState fadeState;
    [SerializeField] private GameObject panel;
    [SerializeField] private Image panelImg;
    [SerializeField] private float alpha;
    [SerializeField] private float speed;

    private void Start()
    {
        panelImg = panel.GetComponent<Image>();
        switch (fadeState)
        {
            case FadeState.FadeIn:
                panelImg.color = new Color(panelImg.color.r, panelImg.color.g, panelImg.color.b, 1.0f);
                break;
            case FadeState.FadeOut:
                panelImg.color = new Color(panelImg.color.r, panelImg.color.g, panelImg.color.b, 0.0f);
                break;
        }
        alpha = panelImg.color.a;
    }
    private void Update()
    {
        switch (fadeState)
        {
            case FadeState.FadeIn:
                fadeState = (FadeIn() == true) ? FadeState.None : fadeState;
                break;
            case FadeState.FadeOut:
                fadeState = (FadeOut() == true) ? FadeState.None : fadeState;
                break;
            case FadeState.None:
                StartCoroutine(DelayDestroy(gameObject, 0.5f));
                break;
        }
    }

    private bool FadeIn()
    {
        alpha -= speed;
        panelImg.color = new Color(panelImg.color.r, panelImg.color.g, panelImg.color.b, alpha);
        
        return (alpha <= 0) ? true : false;
    }
    private bool FadeOut()
    {
        alpha += speed;
        panelImg.color = new Color(panelImg.color.r, panelImg.color.g, panelImg.color.b, alpha);
        
        return (alpha >= 1) ? true : false;
    }

    public void SetFadeState(FadeState state)
    {
        fadeState = state;
    }
}
