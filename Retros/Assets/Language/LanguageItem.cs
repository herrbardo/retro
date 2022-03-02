using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LanguageValue", menuName = "ScriptableObjects/Herrbardo/LanguageValue", order = 1)]
public class LanguageItem : ScriptableObject
{
    [SerializeField] public string Code;
    [SerializeField]public string English;
    [SerializeField]public string Spanish;
}
