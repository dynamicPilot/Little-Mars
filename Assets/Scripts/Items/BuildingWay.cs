using UnityEngine;

[System.Serializable]
public class BuildingWay
{
    [Header("Way")]
    [SerializeField] private BuildingWayStep[] way;
    public BuildingWayStep[] Way { get { return way; } }

    [Header("Icon Parts")]
    [SerializeField] private ItemIcons[] iconParts;
    public ItemIcons[] IconParts { get { return iconParts; } }
}
