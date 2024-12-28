using LittleMars.Common.Interfaces;

namespace LittleMars.Common.Signals
{
    public struct MapSlotsAreReadySignal { }

    public struct StartBuildingPlacementSignal { }
    public struct BeginBuildingDragSignal{ }
    public struct EndBuildingDragSignal { }

    public struct BuildingControllerOpenSignal
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
        public States State { get; set; }
        public OperationMode Mode { get; set; }
    }
}
