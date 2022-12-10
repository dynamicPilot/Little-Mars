using LittleMars.Common;
using System.Collections.Generic;


namespace LittleMars.Models
{
    /// <summary>
    /// Help to create Dictionaries ProductionManager.
    /// </summary>
    public class ProductionHelper
    {
        public void FillProduction(out Dictionary<Resource, Dictionary<Period, float>> production)
        {
            production = new Dictionary<Resource, Dictionary<Period, float>>();

            for (int i = 0; i < (int)Resource.all; i++)
            {
                var resource = (Resource)i;
                production.Add(resource, new Dictionary<Period, float>());
                production[resource][Period.day] = 0f;
                production[resource][Period.night] = 0f;
            }
        }

        public void FillResourceDict(out Dictionary<Resource, float> dict)
        {
            dict = new Dictionary<Resource, float>();

            for (int i = 0; i < (int)Resource.all; i++)
            {
                var resource = (Resource)i;
                dict.Add(resource, 0f);
            }
        }

        public void FillResourceDictWithResourceUnits(ResourceUnit<float>[] units,
            out Dictionary<Resource, float> dict)
        {
            FillResourceDict(out dict);

            foreach (ResourceUnit<float> unit in units)
            {
                dict[unit.Type] += unit.Amount;
            }
        }

    }
}
