using LittleMars.Buildings;
using LittleMars.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleMars.Common.Signals
{
    public struct MapSlotsAreReadySignal { }

    public struct StartBuildingPlacementSignal { }

    public struct BuildingControllerSignal
    {
        public IBuildingFacade BuildingFacade { get; set; }
    }

    public struct AddBuildingSignal
    {
        public IBuildingFacade BuildingFacade { get; set; }
    }

    public struct RemoveBuildingSignal
    {
        public IBuildingFacade BuildingFacade { get; set; }
    }

    public struct BuildingStateChangedSignal
    {
        public IBuildingFacade BuildingFacade { get; set; }
    }

    public struct TryChangeBuildingStateSignal
    {
        public IBuildingFacade BuildingFacade { get; set; }
        public ProductionState State { get; set; }
        public OperationMode Mode { get; set; }
    }
}
