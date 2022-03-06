using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ShowTutorialMessages", 1f);
    }

    void ShowTutorialMessages()
    {
        int round = GameManager.Instance.GetCurrentRound();
        string codeTutorial = "TUTORIAL_" + round;

        try
        {
            string message = LanguageManager.Instance.GetValueFor(codeTutorial);
            UIEvents.GetInstance().OnCentralMessagePosted(message, false);
        }
        catch(LanguageCodeDoesNotExistsException ex)
        {
            Debug.Log(ex.Message);
        }
    }
}
