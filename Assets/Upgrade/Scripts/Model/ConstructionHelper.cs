using LittleMars.Common;
using LittleMars.Models;

namespace LittleMars.Model
{
    /// <summary>
    /// A script to control resources for construction of the building.
    /// </summary>
    public class ConstructionHelper
    {
        readonly ProductionManager _production;

        public ConstructionHelper(ProductionManager production)
        {
            _production = production;
        }

        public bool CheckResources(BuildingObject building)
        {
            // check resources
            return _production.HasResources(building.Construction.ResourcesForBuilding);
        }

        public void Accept(BuildingObject building)
        {
            _production.UpdateResourcesAmount(building.Construction.ResourcesForBuilding, 
                States.off);
        }
    }
}
