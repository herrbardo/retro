using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BtnLanguage : MonoBehaviour
{
    public Image ImageComponent;
    public Sprite ENGIcon;
    public Sprite SPAIcon;

    void Start()
    {
        
    }

    void Update()
    {
        ChooseImage();
    }

    public void ButtonClick()
    {
        Languages lang =  LanguageManager.Instance.GetCurrentLanguage();
        lang = EnumManager.Next<Languages>(lang);
        LanguageManager.Instance.SetLanguage(lang);
    }

    void ChooseImage()
    {
        Languages lang =  LanguageManager.Instance.GetCurrentLanguage();

        switch(lang)
        {
            case Languages.ENGLISH:
                ImageComponent.sprite = ENGIcon;
            break;

            case Languages.SPANISH:
                ImageComponent.sprite = SPAIcon;
            break;
        }
    }
}
