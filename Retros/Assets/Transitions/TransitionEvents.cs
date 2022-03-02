using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionEvents
{
    private static TransitionEvents instance;

    private TransitionEvents()
    {

    }

    public static TransitionEvents GetInstance()
    {
        if(instance == null)
            instance = new TransitionEvents();
        return instance;
    }

    public delegate void TransitionToSceneDelegate(string sceneName);
    public delegate void TransitionFinishedDelegate(TransitionMode mode);
    public delegate void TransitionCreatedDelegate(TransitionMode mode);

    public event TransitionToSceneDelegate TransitionToScene;
    public event TransitionFinishedDelegate TransitionFinished;
    public event TransitionCreatedDelegate TransitionCreated;

    public void OnTransitionToScene(string sceneName)
    {
        if(TransitionToScene != null)
            TransitionToScene(sceneName);
    }

    public void OnTransitionFinished(TransitionMode mode)
    {
        if(TransitionFinished != null)
            TransitionFinished(mode);
    }

    public void OnTransitionCreated(TransitionMode mode)
    {
        if(TransitionCreated != null)
            TransitionCreated(mode);
    }

}
