using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text RoundText;
    [SerializeField] TMP_Text CentralMessage;
    [SerializeField] Button RestartButton;
    [SerializeField] TMP_Text RoundTime;
    [SerializeField] GameObject CentralMessageContainer;
    [SerializeField] GameObject UIMenuCanvas;

    private void Awake()
    {
        UIEvents.GetInstance().CentralMessagePosted += MessagePosted;
        UIEvents.GetInstance().UpdateRoundTime += SetTime;
    }

    private void OnDestroy()
    {
        UIEvents.GetInstance().CentralMessagePosted -= MessagePosted;
        UIEvents.GetInstance().UpdateRoundTime -= SetTime;
    }

    private void Start()
    {
        string roundWord = LanguageManager.Instance.GetValueFor("ROUND");
        RoundText.text = roundWord + " " + GameManager.Instance.GetRound();
        CentralMessageContainer.gameObject.SetActive(false);
        UIMenuCanvas.SetActive(false);
    }

    public void MessagePosted(string message, bool hideAfterAWhile)
    {
        CentralMessageContainer.gameObject.SetActive(true);
        CentralMessage.text = message;

        if(hideAfterAWhile)
            Invoke("HideCentralMessage", 2f);
    }

    void HideCentralMessage()
    {
        CentralMessageContainer.gameObject.SetActive(false);
    }

    public void Restart()
    {
        GameManager.Instance.RestartFromZero();
    }

    public void SetTime(int time)
    {
        RoundTime.text = time.ToString();
    }

    public void SwitchMenuView()
    {
        if(UIMenuCanvas.active)
            UIMenuCanvas.SetActive(false);
        else
            UIMenuCanvas.SetActive(true);
    }

    public void ExitToMenu()
    {
        TransitionEvents.GetInstance().OnTransitionToScene("MainMenu");
        GlobalDJ.Instance.PlaySong(0);
    }
}
