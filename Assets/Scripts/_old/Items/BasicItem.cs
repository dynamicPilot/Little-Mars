using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Basic Item", menuName = "Items/New Basic Item")]
public class BasicItem : ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] private string itemName;
    public string ItemName { get { return itemName; } }

    [SerializeField] private FullStoryForDescription itemNameByLangs;
    public FullStoryForDescription ItemNameByLangs { get { return itemNameByLangs; } }

    //[SerializeField] private bool isAvailableOnStart;

    [SerializeField] private Sprite icon;
    public Sprite Icon { get { return icon; } }

    [Header("Current State")]
    [SerializeField] private bool isAvailable;
    public bool IsAvailable { get { return isAvailable; } set { isAvailable = value; } }

    [SerializeField] private int availableAmount;
    public int AvailableAmount { get { return availableAmount; } set { availableAmount = value; } }

    [SerializeField] private float multiplier;
    public float Multiplier { get { return multiplier; } set { multiplier = value; } }

    [SerializeField] private State.PRIORITY priority;
    public State.PRIORITY Priority { get { return priority; } set { priority = value; } }

    [Header("Need To Build And Operate")]
    [SerializeField] private List<ProductionUnit> buildingNeeds;
    public List<ProductionUnit> BuildingNeeds { get { return buildingNeeds; } }

}
