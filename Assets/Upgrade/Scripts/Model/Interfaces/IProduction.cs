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
        public void UpdateProduction(Dictionary<Resource, Dictionary<Period, float>> production, States state);
        public void UpdateNeeds(ResourceUnit<float>[] needs, States state);
        public bool HasResources(ResourceUnit<float>[] needs);
    }
}
