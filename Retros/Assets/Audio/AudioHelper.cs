using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioHelper
{
    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        return StartFade(audioSource, duration, targetVolume, null);
    }

    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume, Action callback)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }

        if(callback != null)
            callback();
        
        yield break;
    }
}
