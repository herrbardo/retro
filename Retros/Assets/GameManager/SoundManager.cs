using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] List<AudioClip> Clips;
    [SerializeField] bool PlayOnAwake;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if(PlayOnAwake)
            PlayRandomClip();
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void PlayRandomClip()
    {
        audioSource.clip = GetRandomClip(Clips);
        audioSource.Play();
    }

    AudioClip GetRandomClip(List<AudioClip> source)
    {
        return source[Random.Range(0, source.Count)];
    }
}
