using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents
{
    private static GameEvents instance;

    private GameEvents()
    {

    }

    public static GameEvents GetInstance()
    {
        if(instance == null)
            instance = new GameEvents();
        return instance;
    }

    public delegate void PlayerHasReachedEnemyPortalDelegate(Queue<Step> stepsRecord);
    public delegate void EnemyHasReachedPlayerPortalDelegate();
    public delegate void RoundTimeoutDelegate();

    public event PlayerHasReachedEnemyPortalDelegate PlayerHasReachedEnemyPortal;
    public event EnemyHasReachedPlayerPortalDelegate EnemyHasReachedPlayerPortal;
    public event RoundTimeoutDelegate RoundTimeout;

    public void OnPlayerHasReachedEnemyPortal(Queue<Step> stepsRecord)
    {
        if(PlayerHasReachedEnemyPortal != null)
            PlayerHasReachedEnemyPortal(stepsRecord);
    }

    public void OnEnemyHasReachedPlayerPortal()
    {
        if(EnemyHasReachedPlayerPortal != null)
            EnemyHasReachedPlayerPortal();
    }

    public void OnRoundTimeout()
    {
        if(RoundTimeout != null)
            RoundTimeout();
    }
}
