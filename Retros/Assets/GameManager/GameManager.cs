using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] int MaxRoundTime;
    [SerializeField] float TimeToRestartAfterDie;
    [SerializeField] public int FinalRound;
    [SerializeField] public int StartRound;
    [NonSerialized] public int CurrentRound;

    float roundTime;
    bool roundFinished;
    List<Queue<Step>> stepsByRound;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            CurrentRound = StartRound;
            DontDestroyOnLoad(this.gameObject);
            InitRoundValues();
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        GameEvents.GetInstance().PlayerHasReachedEnemyPortal += PlayerHasReachedEnemyPortal;
        GameEvents.GetInstance().EnemyHasReachedPlayerPortal += EnemyHasReachedPlayerPortal;
        GameEvents.GetInstance().GameExited += GameExited;
        GameEvents.GetInstance().PlayerHasBeenKilled += PlayerHasBeenKilled;
        TransitionEvents.GetInstance().TransitionFinished += TransitionFinished;
        this.stepsByRound = new List<Queue<Step>>();

        if(GlobalDJ.Instance != null)
            GlobalDJ.Instance.PlaySong(1);
    }

    private void OnDestroy()
    {
        GameEvents.GetInstance().PlayerHasReachedEnemyPortal -= PlayerHasReachedEnemyPortal;
        GameEvents.GetInstance().EnemyHasReachedPlayerPortal -= EnemyHasReachedPlayerPortal;
        GameEvents.GetInstance().GameExited -= GameExited;
        GameEvents.GetInstance().PlayerHasBeenKilled -= PlayerHasBeenKilled;
        TransitionEvents.GetInstance().TransitionFinished -= TransitionFinished;
    }

    private void Update()
    {
        CheckTimeout();
    }

    void CheckTimeout()
    {
        if(roundFinished)
            return;

        if(roundTime > 0)
        {
            roundTime -= Time.deltaTime;
            UIEvents.GetInstance().OnUpdateRoundTime((int) roundTime);
        }
        else
            PlayerTimeout();
    }

    void PlayerHasReachedEnemyPortal(Queue<Step> stepsRecord)
    {
        roundFinished = true;
        this.stepsByRound.Add(stepsRecord);
        CurrentRound++;
        string message = LanguageManager.Instance.GetValueFor("MSG_VICTORY");
        UIEvents.GetInstance().OnCentralMessagePosted(message, false);

        if(CurrentRound < FinalRound)
            Invoke("Restart", 3f);
    }

    void EnemyHasReachedPlayerPortal()
    {
        roundFinished = true;
        string message = LanguageManager.Instance.GetValueFor("MSG_YOU_ARE_WORSE");
        UIEvents.GetInstance().OnCentralMessagePosted(message, false);

        Invoke("AfterDeath", TimeToRestartAfterDie);
    }

    void PlayerTimeout()
    {
        roundFinished = true;
        GameEvents.GetInstance().OnRoundTimeout();
        string message = LanguageManager.Instance.GetValueFor("MSG_TOO_SLOW");
        UIEvents.GetInstance().OnCentralMessagePosted(message, false);
    }

    void Restart()
    {
        TransitionEvents.GetInstance().OnTransitionToScene(GetSceneName());
    }

    public List<Queue<Step>> GetStepsByRound()
    {
        return this.stepsByRound;
    }

    public void RestartFromZero(bool reboot)
    {
        CurrentRound = 1;

        if(reboot)
            TransitionEvents.GetInstance().OnTransitionToScene(GetSceneName());
    }

    void InitRoundValues()
    {
        this.roundFinished = false;
        this.roundTime = MaxRoundTime + 1;
    }

    void TransitionFinished(TransitionMode mode)
    {
        InitRoundValues();
    }

    void GameExited()
    {
        RestartFromZero(false);
        GlobalDJ.Instance.PlaySong(0);
        Invoke("FinallyDie", .1f);
    }

    void FinallyDie()
    {
        Destroy(this.gameObject);
    }

    void PlayerHasBeenKilled()
    {
        string message = LanguageManager.Instance.GetValueFor("YOU_DEAD");
        UIEvents.GetInstance().OnCentralMessagePosted(message, false);

        Invoke("AfterDeath", TimeToRestartAfterDie);
    }

    void AfterDeath()
    {
        RestartFromZero(true);
    }

    string GetSceneName()
    {
        if(CurrentRound > 10 && CurrentRound <= 20)
            return "MedievalFields";
        else
            return "CloudCity";
    }
}
