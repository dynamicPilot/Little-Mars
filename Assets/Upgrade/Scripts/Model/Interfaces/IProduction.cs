using LittleMars.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleMars.Model.Interfaces
{
    public interface IProduction
    {
        public void UpdateProduction(Dictionary<Resource, Dictionary<Period, float>> production, ProductionState state);
        public void UpdateNeeds(ResourceUnit<float>[] needs, ProductionState state);
        public bool HasResourcesForBuildingToOn(ResourceUnit<float>[] needs);
    }
}
