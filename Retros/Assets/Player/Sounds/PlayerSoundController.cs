using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    [SerializeField] PlayerController PlayerController;
    [SerializeField] AudioSource StepsSource;
    [SerializeField] List<AudioClip> FootSteps;
    
    void Start()
    {
        
    }

    void Update()
    {
    }

    void Step()
    {
        StepsSource.clip = GetRandomClip(FootSteps);
        StepsSource.Play();
    }

    AudioClip GetRandomClip(List<AudioClip> source)
    {
        return source[Random.Range(0, source.Count)];
    }
}
