using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnSwitchManager : MonoBehaviour
{
    [SerializeField] string PrefsVariableName;
    [SerializeField] Sprite EnableSprite;
    [SerializeField] Sprite DisabledSprite;

    bool enabled;

    void Start()
    {
        int value = PlayerPrefs.GetInt(PrefsVariableName, 1);
        enabled = value == 1;
        SetImage();
    }

    void SetImage()
    {
        Image imageComponent = GetComponent<Image>();
        imageComponent.sprite = (enabled) ? EnableSprite : DisabledSprite;
    }

    public void Click()
    {
        if(enabled)
            PlayerPrefs.SetInt(PrefsVariableName, 0);
        else
            PlayerPrefs.SetInt(PrefsVariableName, 1);
        
        enabled = !enabled;
        SetImage();
        UIEvents.GetInstance().OnUIButtonPressed(this.gameObject.name, enabled);
    }
}
