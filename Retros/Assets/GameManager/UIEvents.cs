using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEvents
{
    private static UIEvents instance;
    
    private UIEvents()
    {

    }

    public static UIEvents GetInstance()
    {
        if(instance == null)
            instance = new UIEvents();

        return instance;
    }

    public delegate void CentralMessagePostedDelegate(string message, bool hideAfterAWhile);
    public delegate void UpdateRoundTimeDelegate(int time);

    public event CentralMessagePostedDelegate CentralMessagePosted;
    public event UpdateRoundTimeDelegate UpdateRoundTime;

    public void OnCentralMessagePosted(string message, bool hideAfterAWhile)
    {
        if(CentralMessagePosted != null)
            CentralMessagePosted(message, hideAfterAWhile);
    }

    public void OnUpdateRoundTime(int time)
    {
        if(UpdateRoundTime != null)
            UpdateRoundTime(time);
    }
}
