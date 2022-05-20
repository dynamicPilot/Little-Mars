using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProductionUnit
{
    [SerializeField] private Inventory.R_TYPE resourseType;
    public Inventory.R_TYPE ResourseType { get { return resourseType; } }

    [Header("Day & Night")]
    [SerializeField] private float dayAmount = 0f;
    public float DayAmount { get { return dayAmount; } }

    [SerializeField] private float nightAmount = 0f;
    public float NightAmount { get { return nightAmount; } }


    [SerializeField] private float multiplier = 1f;
    public float Multiplier { get { return multiplier; } }

    public ProductionUnit()
    {
        resourseType = Inventory.R_TYPE.energy;
        dayAmount = 0f;
        multiplier = 1f;
    }

    public ProductionUnit(Inventory.R_TYPE newType, float newAmount = 0f)
    {
        resourseType = newType;
        dayAmount = newAmount;
        nightAmount = newAmount;
        multiplier = 1f;
    }

    public ProductionUnit(Inventory.R_TYPE newType, float newDayAmount, float newNightAmount, float newMultiplier = 1f)
    {
        resourseType = newType;
        dayAmount = newDayAmount;
        nightAmount = newNightAmount;
        multiplier = newMultiplier;
    }

    public void ChangeDayAmount(float newAmount)
    {
        dayAmount += newAmount;

        //if (dayAmount < 0)
        //    dayAmount = 0;
    }

    public void ChangeNightAmount(float newAmount)
    {
        nightAmount += newAmount;

        //if (nightAmount < 0)
        //    nightAmount = 0;
    }

    public void ChangeMultiplier(float newMultiplier)
    {
        multiplier *= newMultiplier;
    }

    public void ChangeResourseType(Inventory.R_TYPE newResourseType)
    {
        resourseType = newResourseType;
        dayAmount = 0f;
        multiplier = 1f;
    }
}

[System.Serializable]
public class BuildingUnit
{
    [SerializeField] private BuildingItem item;
    [SerializeField] private int amount = 0;
    [SerializeField] private float multiplier = 1f;

    public BuildingItem Item { get { return item; } }
    public int Amount { get { return amount; } }
    public float Multiplier { get { return multiplier; } }

    public BuildingUnit()
    {
        item = null;
        amount = 0;
        multiplier = 1f;
    }

    public BuildingUnit(BuildingItem newItem, int newAmount, float newMultiplier = 1f)
    {
        item = newItem;
        amount = newAmount;
        multiplier = newMultiplier;
    }
}