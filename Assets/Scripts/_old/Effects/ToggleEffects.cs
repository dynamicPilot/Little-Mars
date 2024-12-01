using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleEffects : MonoBehaviour
{
    [SerializeField] private Color[] colors;
    [SerializeField] private Image backgroundImage;

    public void ChangeColor(bool isOn)
    {
        if (colors == null)
        {
            return;
        }
        else if (colors.Length < 2)
        {
            return;
        }

        if (isOn)
        {
            backgroundImage.color = colors[0];
        }
        else
        {
            backgroundImage.color = colors[1];
        }
    }

}
