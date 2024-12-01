using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomFunctions
{
    public static Vector3 ScreenToWorld(Camera camera, Vector3 position)
    {
        position.z = camera.nearClipPlane;
        return camera.ScreenToViewportPoint(position);
    }

    public static List<ProductionUnit> MakeFullCopyOfListOfProductionUnits (List<ProductionUnit> originalList)
    {
        List<ProductionUnit> newList = new List<ProductionUnit>();

        foreach (ProductionUnit unit in originalList)
        {
            newList.Add(new ProductionUnit(unit.ResourseType, unit.DayAmount, unit.NightAmount, unit.Multiplier));
        }

        return newList;
    }

    public static ProductionUnit[] MakeFullCopyOfArrayOfProductionUnits(ProductionUnit[] originalList)
    {
        List<ProductionUnit> newList = new List<ProductionUnit>();

        foreach (ProductionUnit unit in originalList)
        {
            newList.Add(new ProductionUnit(unit.ResourseType, unit.DayAmount, unit.NightAmount, unit.Multiplier));
        }

        return newList.ToArray();
    }

    public static List<BuildingUnit> MakeFullCopyOfListOfBuildingUnits(List<BuildingUnit> originalList)
    {
        List<BuildingUnit> newList = new List<BuildingUnit>();

        foreach (BuildingUnit unit in originalList)
        {
            newList.Add(new BuildingUnit(unit.Item, unit.Amount, unit.Multiplier));
        }

        return newList;
    }

    public static BuildingUnit[] MakeFullCopyOfArrayOfBuildingUnits(BuildingUnit[] originalList)
    {
        List<BuildingUnit> newList = new List<BuildingUnit>();

        foreach (BuildingUnit unit in originalList)
        {
            newList.Add(new BuildingUnit(unit.Item, unit.Amount, unit.Multiplier));
        }

        return newList.ToArray();
    }

    public static List<int> MakeFullCopyOfListOfInt(List<int> originalList)
    {
        List<int> newList = new List<int>();

        foreach (int unit in originalList)
        {
            newList.Add(unit);
        }

        return newList;
    }

    public static List<Inventory.B_TYPE> MakeFullCopyOfListOfBuildingsType(List<Inventory.B_TYPE> originalList)
    {
        List<Inventory.B_TYPE> newList = new List<Inventory.B_TYPE>();

        foreach (Inventory.B_TYPE unit in originalList)
        {
            newList.Add(unit);
        }

        return newList;
    }

    //public static List<ProductionUnit> MakeFullCopyOfListOfSlots(List<MapSlot> originalList)
    //{
    //    List<MapSlot> newList = new List<MapSlot>();

    //    //foreach (MapSlot unit in originalList)
    //    //{
    //    //    newList.Add(new MapSlot());
    //    //}

    //    return newList;
    //}
}
