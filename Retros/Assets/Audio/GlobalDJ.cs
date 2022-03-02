using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GlobalDJ : MonoBehaviour
{
    public static GlobalDJ Instance;
    [SerializeField] public float EffectsMaxVolume;
    [SerializeField] public float EffectsMinVolume;
    [SerializeField] public float MusicMaxVolume;
    [SerializeField] public float MusicMinVolume;
    [SerializeField] public AudioMixer GlobalMixer;

    [SerializeField] AudioClip[] TrackList;
    [SerializeField] float TimeBewtweenSongs;

    AudioSource audioSource;
    float originalVolume;
    int localIndexSong;

    void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
        
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad (gameObject);
    }

    void Start()
    {
        originalVolume = audioSource.volume;
        CheckAudioSettings();
    }

    public void PlaySong(int indexSong)
    {
        localIndexSong = indexSong;
        float time = 0;
        if(audioSource.isPlaying)
        {
            time = 5;
            StartCoroutine(AudioHelper.StartFade(this.audioSource, time, 0f));
        }
        
        Invoke("FinallyPlay", time);
    }

    void FinallyPlay()
    {
        audioSource.clip = TrackList[localIndexSong];
        audioSource.volume = 0;
        audioSource.Play();
        StartCoroutine(AudioHelper.StartFade(this.audioSource, 5, originalVolume));
    }

    void UICommand_Button(UICommand command, bool value)
    {
        if(command == UICommand.BtnSound)
            SwitchVolume("EffectsVolume", EffectsMaxVolume, EffectsMinVolume, value);
        else if(command == UICommand.BtnMusic)
            SwitchVolume("MusicVolume", MusicMaxVolume, MusicMinVolume, value);
    }

    void SwitchVolume(string variableName, float maxVolume, float minVolume, bool value)
    {
        if(value)
            GlobalMixer.SetFloat(variableName, maxVolume);
        else
            GlobalMixer.SetFloat(variableName, minVolume);
    }

    void CheckAudioSettings()
    {
        int sound = PlayerPrefs.GetInt("Herrbardo_Settings_Sound", 1);
        float puta = (sound == 0) ? EffectsMinVolume : EffectsMaxVolume;
        GlobalMixer.SetFloat("EffectsVolume", puta);

        int music = PlayerPrefs.GetInt("Herrbardo_Settings_Music", 1);
        float puta2 = (music == 0) ? MusicMinVolume : MusicMaxVolume;
        GlobalMixer.SetFloat("MusicVolume", puta2);
    }

    public bool IsBusy()
    {
        return audioSource.isPlaying;
    }

    public void StopMusic()
    {
        StartCoroutine(AudioHelper.StartFade(this.audioSource, 5f, 0f));
    }
}
