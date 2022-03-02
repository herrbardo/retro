using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashManager : MonoBehaviour
{
    [SerializeField] float TimeToGoNextScene;
    [SerializeField] string NextSceneName;
    [SerializeField] bool PlayMusicAtStart;

    private void Start()
    {
        Invoke("GoToMainMenu", TimeToGoNextScene);
        if(PlayMusicAtStart)
            GlobalDJ.Instance.PlaySong(0);
    }

    void GoToMainMenu()
    {
        TransitionEvents.GetInstance().OnTransitionToScene(NextSceneName);
    }
}
