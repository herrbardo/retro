using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TransitionVariables
{
    static TransitionVariables instance;

    private TransitionVariables()
    {

    }

    public static TransitionVariables GetInstance()
    {
        if(instance == null)
            instance = new TransitionVariables();
        return instance;
    }

    DateTime transitioningLastDate;


    public void ReportTransition()
    {
        transitioningLastDate = DateTime.Now;
    }

    public bool IsThereAnyTransition()
    {
        TimeSpan difference = DateTime.Now - transitioningLastDate;
        return difference.TotalMilliseconds < 500;
    }
}
