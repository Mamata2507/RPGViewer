using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InfoManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField labelInput;
    [SerializeField] private TMP_Text labelText;

    [SerializeField] private TMP_InputField viewInput;
    [SerializeField] private Toggle lightsToggle;

    [SerializeField] private LightManager LightManager;


    private void Update()
    {
        UpdateLabel();
        UpdateView();
        UpdateLights();
    }

    private void UpdateLabel()
    {
        labelText.text = labelInput.text;
    }

    private void UpdateView()
    {
        
    }

    private void UpdateLights()
    {
        LightManager.UpdateMyLight(lightsToggle.isOn);
    }
}
