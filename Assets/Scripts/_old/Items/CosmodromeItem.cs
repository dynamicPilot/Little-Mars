using UnityEngine;

[CreateAssetMenu(fileName = "New Cosmodrome", menuName = "Items/New Cosmodrome")]
public class CosmodromeItem : BuildingItem
{
    [Header("Cosmodrome Options")]
    [SerializeField] private int rocketArriveHour;
    public int RocketArriveHour { get { return rocketArriveHour; } }
}
