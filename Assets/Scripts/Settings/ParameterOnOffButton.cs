using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParameterOnOffButton : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Sprite isOnSprite;
    [SerializeField] private Sprite isOffSprite;

    [Header("UI")]
    [SerializeField] private Image image;

    bool isOn = false;

    public void SetValue(bool value)
    {
        isOn = value;
        UpdateSprite();
    }

    public void ChangeValue()
    {
        isOn = !isOn;
        UpdateSprite();
    }

    public bool GetValue()
    {
        return isOn;
    }

    void UpdateSprite()
    {
        if (isOn)
        {
            image.sprite = isOnSprite;
        }
        else
        {
            image.sprite = isOffSprite;
        }
    }

}
