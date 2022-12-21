using LittleMars.Map;
using System.Collections.Generic;
using UnityEngine;

namespace LittleMars.Common.Interfaces
{
    public interface IModelFacade
    {
        List<List<MapSlotExtended>> MapSlots();
        void StartBuildingPlacement(BuildingObject buildingObject, Indexes indexes, Vector2 position);
        void CallBuildingController(IBuildingFacade building);
    }

    public interface IViewSlotFacade
    { 
    
    }


}

