using LittleMars.Map;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

namespace LittleMars.Common.Interfaces
{
    public interface IModelFacade
    {
        List<List<MapSlotExtended>> MapSlots();
        void StartBuildingPlacement(BuildingObject buildingObject, Indexes indexes, Vector2 position);
        void TryChangeBuildingState(IBuildingFacade building, ProductionState state);
    }

    public interface IViewSlotFacade
    { 
    
    }


}

