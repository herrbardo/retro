using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum TransitionMode
{
    FadeToLight = 0,
    FadeToBlack = 1
}

public class TransitionManager : MonoBehaviour
{
    public TransitionMode Mode;
    public float Duration;
    public bool EnableFade;
    public Color Color = Color.black;
    [SerializeField] SpriteRenderer ParentSprite;

    List<SpriteRenderer> sprites;
    bool iamDead;

    void Start()
    {
        Init();
        TransitionEvents.GetInstance().OnTransitionCreated(Mode);
        StartCoroutine(StartFade(ParentSprite, Duration, Mode, FadeFinished));
    }

    public void Init()
    {
        sprites = new List<SpriteRenderer>(){ ParentSprite };

        foreach (SpriteRenderer item in sprites)
        {
            item.color = Color;
            item.color = InitColor(item.color);
        }
    }

    void Update()
    {
        if(!iamDead)
            TransitionVariables.GetInstance().ReportTransition();
    }

    Color InitColor(Color baseColor)
    {
        Color destinationColor = new Color();
        if(Mode == TransitionMode.FadeToLight)
            destinationColor = new Color(baseColor.r, baseColor.g, baseColor.b, 1);
        else
            destinationColor = new Color(baseColor.r, baseColor.g, baseColor.b, 0);

        return destinationColor;
    }

    public static IEnumerator StartFade(SpriteRenderer sprite, float duration, TransitionMode mode, Action callback)
    {
        float targetAlpha = mode == TransitionMode.FadeToBlack ? 1 : 0;
        float currentTime = 0;
        float start = sprite.color.a;

        while(currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float alpha = Mathf.Lerp(start, targetAlpha, currentTime / duration);
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
            yield return null;
        }

        if(callback != null)
            callback();
        
        yield break;
    }

    void FadeFinished()
    {
        TransitionEvents.GetInstance().OnTransitionFinished(Mode);
        if(Mode == TransitionMode.FadeToLight)
            Die();
    }

    void Die()
    {
        iamDead = true;
    }
}
