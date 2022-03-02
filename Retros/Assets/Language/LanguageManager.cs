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

        //LoadValues();
    }

    // void LoadValues()
    // {
    //     Items = new List<LanguageItem>();
    //     string[] assetNames = AssetDatabase.FindAssets("t:LanguageItem", new[] { "Assets/Language/Values" });
    //     foreach (string SOName in assetNames)
    //     {
    //         string SOpath = AssetDatabase.GUIDToAssetPath(SOName);
    //         LanguageItem item = AssetDatabase.LoadAssetAtPath<LanguageItem>(SOpath);
    //         Items.Add(item);
    //     }
    // }

    public string GetValueFor(string code)
    {
        LanguageItem item = Items.Where(i => i.Code == code).FirstOrDefault();
        if(item == null)
            return "CODE '" + code + "' does not exists";
        
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
