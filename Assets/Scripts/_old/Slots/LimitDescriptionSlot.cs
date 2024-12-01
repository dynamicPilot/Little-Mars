using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LimitDescriptionSlot : MonoBehaviour
{
    [SerializeField] private Text text;

    public void SetText(string newText)
    {
        text.text = newText;
    }
}
