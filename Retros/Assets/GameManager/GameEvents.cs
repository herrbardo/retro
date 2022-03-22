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
    public delegate void GameExitedDelegate();
    public delegate void GameStartedDelegate();
    public delegate void PlayerHasBeenKilledDelegate();
    public delegate void EnemyLeftPortalDelegate();

    public event PlayerHasReachedEnemyPortalDelegate PlayerHasReachedEnemyPortal;
    public event EnemyHasReachedPlayerPortalDelegate EnemyHasReachedPlayerPortal;
    public event RoundTimeoutDelegate RoundTimeout;
    public event GameExitedDelegate GameExited;
    public event GameStartedDelegate GameStarted;
    public event PlayerHasBeenKilledDelegate PlayerHasBeenKilled;
    public event EnemyLeftPortalDelegate EnemyLeftPortal;

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

    public void OnGameExited()
    {
        if(GameExited != null)
            GameExited();
    }

    public void OnGameStarted()
    {
        if(GameStarted != null)
            GameStarted();
    }

    public void OnPlayerHasBeenKilled()
    {
        if(PlayerHasBeenKilled != null)
            PlayerHasBeenKilled();
    }

    public void OnEnemyLeftPortal()
    {
        if(EnemyLeftPortal != null)
            EnemyLeftPortal();
    }
}
