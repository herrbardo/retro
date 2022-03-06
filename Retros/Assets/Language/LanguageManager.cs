using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//using UnityEditor;
using System;

public class LanguageManager:MonoBehaviour
{
    public static LanguageManager Instance;
    public static bool IsReady;
    [SerializeField] List<LanguageItem> Items;

    private Languages currentLanguage;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            Init();
            DontDestroyOnLoad(this.gameObject);
            IsReady = true;
        }
        else
            Destroy(this.gameObject);
    }

    private void Init()
    {
        string lang = PlayerPrefs.GetString("HERRBARDO_LANGUAGE_PLUGIN_SELECTED_LANGUAGE", "ENGLISH");
        if(lang == "ENGLISH")
            SetLanguage(Languages.ENGLISH);
        else if(lang == "SPANISH")
            SetLanguage(Languages.SPANISH);
    }

    public string GetValueFor(string code)
    {
        LanguageItem item = Items.Where(i => i.Code == code).FirstOrDefault();
        if(item == null)
            throw new LanguageCodeDoesNotExistsException(code);
        
        switch(this.currentLanguage)
        {
            case Languages.SPANISH:
                return item.Spanish;
            break;

            default:
                return item.English;
            break;
        }
    }

    public void SetLanguage(Languages newLanguage)
    {
        this.currentLanguage = newLanguage;
        PlayerPrefs.SetString("HERRBARDO_LANGUAGE_PLUGIN_SELECTED_LANGUAGE", newLanguage.ToString());
    }

    public Languages GetCurrentLanguage()
    {
        return this.currentLanguage;
    }
}
