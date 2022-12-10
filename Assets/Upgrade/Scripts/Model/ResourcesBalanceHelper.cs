using LittleMars.Buildings;
using LittleMars.Common;
using LittleMars.Common.Interfaces;
using System.Collections.Generic;

namespace LittleMars.Models
{
    public class ResourcesBalanceHelper
    {
        Dictionary<Priority, List<IBuildingFacade>> _buildingsByPriority = 
            new Dictionary<Priority, List<IBuildingFacade>>();

        OperationManager _operation;

        public ResourcesBalanceHelper(OperationManager operation)
        {
            _operation = operation;
        }

        public void AddBuilding(IBuildingFacade building)
        {
            var priority = building.Priority();
            if (!_buildingsByPriority.ContainsKey(priority))
                _buildingsByPriority[priority] = new List<IBuildingFacade>();
            _buildingsByPriority[priority].Add(building);
        }

        public void HelpBalanceResource(Resource resource, Period period, int hour)
        {
            foreach (Priority key in _buildingsByPriority.Keys)
            {
                foreach(BuildingFacade building in _buildingsByPriority[key])
                {
                    if (building.State() == ProductionState.off) continue;

                    if (building.HasNeedForThisResource(resource, period))
                    {
                        _operation.TryChangeBuildingState(building, ProductionState.off, OperationMode.forcedAuto);
                        return;
                    }                       
                }
            }
        }
    }
}
