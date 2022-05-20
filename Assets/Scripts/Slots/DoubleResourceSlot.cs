using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoubleResourceSlot : ResourceSlot
{
    [SerializeField] private Text secondText;

    public void SetDoubleSlot(Inventory.R_TYPE newType, float newAmount, float newSecondAmount)
    {
        SetSlot(newType, newAmount, "+");

        secondText.text = "-" + newSecondAmount;
    }
}
