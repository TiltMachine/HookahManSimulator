using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextLocaliserUI : MonoBehaviour
{
    TextMeshProUGUI textField;
    public string key;
    void Start()
    {
        Localise();
        MainMenu.onLanguageChange += OnLanguageChange;
    }

    private void OnLanguageChange()
    {
        Localise();
    }
    
    private void OnDestroy() {
        MainMenu.onLanguageChange -= OnLanguageChange;
    }
    public void Localise(){
        textField = GetComponent<TextMeshProUGUI>();
        string value = LocalisationSystem.GetLocalisedValue(key);
        textField.text = value;
    }
}
