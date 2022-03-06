using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] int MaxRoundTime;

    int round;
    float roundTime;
    bool roundFinished;
    Queue<Step> currentSteps;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            this.round = 1;
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
        TransitionEvents.GetInstance().TransitionFinished += TransitionFinished;
        GlobalDJ.Instance.PlaySong(1);
    }

    private void OnDestroy()
    {
        GameEvents.GetInstance().PlayerHasReachedEnemyPortal -= PlayerHasReachedEnemyPortal;
        GameEvents.GetInstance().EnemyHasReachedPlayerPortal -= EnemyHasReachedPlayerPortal;
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
        this.currentSteps = stepsRecord;
        round++;
        string message = LanguageManager.Instance.GetValueFor("MSG_VICTORY");
        UIEvents.GetInstance().OnCentralMessagePosted(message, false);
        Invoke("Restart", 3f);
    }

    void EnemyHasReachedPlayerPortal()
    {
        roundFinished = true;
        string message = LanguageManager.Instance.GetValueFor("MSG_YOU_ARE_WORSE");
        UIEvents.GetInstance().OnCentralMessagePosted(message, false);
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
        TransitionEvents.GetInstance().OnTransitionToScene("SampleScene");
    }

    public Queue<Step> GetSteps()
    {
        return this.currentSteps;
    }

    public int GetRound()
    {
        return  round;
    }

    public void RestartFromZero(bool reboot)
    {
        this.currentSteps = null;
        this.round = 1;

        if(reboot)
            TransitionEvents.GetInstance().OnTransitionToScene("SampleScene");
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

    public int GetCurrentRound()
    {
        return round;
    }
}
