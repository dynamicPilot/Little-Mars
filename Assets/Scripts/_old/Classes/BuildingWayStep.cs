using UnityEngine;

[System.Serializable]
public class BuildingWayStep
{
    [SerializeField] private int stepDirection;
    [SerializeField] private bool isBackStep = false;
    [SerializeField] private bool isSingleSlotBuilding = false;

    public int StepDirection { get { return stepDirection; } }
    public bool IsBackStep { get { return isBackStep; } }
    public bool IsSingleSlotBuilding { get { return isSingleSlotBuilding; } }

    public int MakeRotation(int rotationIndex)
    {
        return (stepDirection + rotationIndex) % 4;
    }
}
