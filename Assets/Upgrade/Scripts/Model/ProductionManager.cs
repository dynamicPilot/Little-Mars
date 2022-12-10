using LittleMars.Buildings;
using LittleMars.Buildings.Parts;
using LittleMars.Common;
using LittleMars.Common.Interfaces;
using LittleMars.Model.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

namespace LittleMars.Models
{
    public class ProductionManager : IProduction
    {
        Dictionary<Resource, float> _needs = null;
        Dictionary<Resource, Dictionary<Period, float>> _production = null;
        Dictionary<Resource, float> _resourcesBalance = null;

        ProductionHelper _helper;
        LevelConditions.Settings _settings;
        Period _period;

        public ProductionManager(ProductionHelper helper, LevelConditions.Settings settings)
        {
            _helper = helper;

            _helper.FillProduction(out _production);
            _helper.FillResourceDict(out _needs);
            _helper.FillResourceDictWithResourceUnits(settings.Resources, out _resourcesBalance);
        }

        public void OnPeriodChanged(Period period)
        {
            if (_period == period) return;
            _period = period;
        }

        public void UpdateBalance(Period period)
        {
            Debug.Log("Update Balance");
        }

        public void UpdateProduction(Dictionary<Resource, Dictionary<Period, float>> production, ProductionState state)
        {
            Debug.Log("ProductionManager: update production.");
            var multiplier = (state == ProductionState.on) ? 1 : -1;
            foreach (Resource resource in production.Keys)
            {
                foreach (Period period in production[resource].Keys)
                    _production[resource][period] += multiplier * production[resource][period];
            }
        }

        public void UpdateNeeds(ResourceUnit<float>[] needs, ProductionState state)
        {
            Debug.Log("ProductionManager: update needs.");
            var multiplier = (state == ProductionState.on) ? 1 : -1;
            foreach(ResourceUnit<float> unit in needs) _needs[unit.Type] += multiplier * unit.Amount;

        }

        public bool HasResourcesForBuildingToOn(ResourceUnit<float>[] needs)
        {
            Debug.Log("ProductionManager: Check resources.");
            foreach (ResourceUnit<float> unit in needs)
            {
                if (_resourcesBalance[unit.Type] < unit.Amount)
                {
                    Debug.Log("No resources for " + unit.Type);
                    return false;
                }
            }
            
                
            return true;
        }

    }
}
