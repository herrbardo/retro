using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class CreditsManager : MonoBehaviour
{
    [SerializeField] TMP_Text CreditsText;
    [SerializeField] float TimeToLeave;
    [SerializeField] TextAsset Credits;

    void Start()
    {
        CreditsText.text = Credits.text;
        Invoke("Leave", TimeToLeave);
    }

    void Leave()
    {
        TransitionEvents.GetInstance().OnTransitionToScene("MainMenu");
    }
}