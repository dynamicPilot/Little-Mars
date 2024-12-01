using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLimitToggle : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Toggle toggle;

    //private void Awake()
    //{
    //    toggle = GetComponent<Toggle>();
    //    toggle.isOn = false;
    //}

    public void SetToggle(string labelText, bool makeIsOn = false)
    {
        text.text = labelText;
        toggle.isOn = makeIsOn;
    }

    public void SetToggleIsOnValue (bool value = true)
    {
        if (toggle.isOn != value)
            toggle.isOn = value;
    }

    public void MakeToggleOn()
    {
        toggle.isOn = true;
    }

    public void MakeToggleOff()
    {
        toggle.isOn = false;
    }

    public void AddScoreToText(string additionText, bool needRemoveOld)
    {
        if (text.text == "")
        {
            return;
        }

        // remove old and add new
        string currentText = text.text;
        int index = currentText.IndexOf("(");

        if (needRemoveOld)
        {
            if (index > 0)
            {
                currentText = currentText.Remove(index - 1);  
            }
        }

        currentText += additionText;
        text.text = currentText;

    }
}
