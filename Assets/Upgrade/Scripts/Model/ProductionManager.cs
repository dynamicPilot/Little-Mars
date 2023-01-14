using LittleMars.Common;
using LittleMars.Common.Signals;
using LittleMars.Model.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LittleMars.Models
{
    public class ProductionManager : IInitializable, IDisposable
    {
        Dictionary<Resource, float> _needs = null;
        Dictionary<Resource, Dictionary<Period, float>> _production = null;
        Dictionary<Resource, float> _resourcesBalance = null;

        ProductionHelper _helper;
        ResourcesBalancer _balancer;
        LevelConditions.Settings _settings;
        SignalBus _signalBus;
        Period _period;

        int _count = 0;
        public ProductionManager(ProductionHelper helper, 
            LevelConditions.Settings settings, SignalBus signalBus, 
            ResourcesBalancer balancer)
        {
            _helper = helper;
            _settings = settings;
            _signalBus = signalBus;
            _balancer = balancer;        
        }

        public void Initialize()
        {
            _helper.FillProduction(out _production);
            _helper.FillResourceDict(out _needs);
            _helper.FillResourceDictWithResourceUnits(_settings.Resources, out _resourcesBalance);

            _signalBus.Subscribe<PeriodChangeSignal>(OnPeriodChanged);
            _signalBus.Subscribe<HourlySignal>(UpdateBalance);

            //Debug.Log("End init");
            _signalBus.Fire(new ResourcesBalanceUpdatedSignal { ResourcesBalance = _resourcesBalance });
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<PeriodChangeSignal>(OnPeriodChanged);
            _signalBus.TryUnsubscribe<HourlySignal>(UpdateBalance);
        }

        private void OnPeriodChanged(PeriodChangeSignal arg)
        {
            var period = arg.Period;
            if (_period == period) return;
            _period = period;
        }

        private void UpdateBalance()
        {
            //Debug.Log("------------- Update Balance -----------------");
            ResourceUnit<float>[] units = new ResourceUnit<float>[(int)Resource.all];
            for (int i = 0; i < (int) Resource.all; i++)
            {
                var resource = (Resource)i;
                //Debug.Log($" Update {resource}. Before {_resourcesBalance[resource]}");
                var delta = _production[resource][_period] - _needs[resource];

                if (_resourcesBalance[resource] + delta < 0)
                {
                    //Debug.Log("Need balance resource : " + resource);
                    BalanceResource(resource);
                    return;
                }
                else
                {
                    units[i] = new ResourceUnit<float>
                    {
                        Type = (Resource)i,
                        Amount = delta
                    };
                    //Debug.Log($" Update {resource}. After {_resourcesBalance[resource]}");
                }
            }
            UpdateResourcesAmount(units, ProductionState.on);
        }

        private void BalanceResource(Resource resource)
        {
            _count++;
            _balancer.HelpBalanceResource(resource);
            UpdateBalance();

            if (_count > 10)
            {
                //Debug.Log("ProductionManager: FORCE STOP");
                return;
            }
        }

        public void UpdateProduction(Dictionary<Resource, Dictionary<Period, float>> production, ProductionState state)
        {
            //Debug.Log("ProductionManager: update production. State " + state);
            var multiplier = (state == ProductionState.on) ? 1 : -1;
            foreach (Resource resource in production.Keys)
            {
                foreach (Period period in production[resource].Keys)
                    _production[resource][period] += multiplier * production[resource][period];
            }
        }

        public void UpdateNeeds(ResourceUnit<float>[] needs, ProductionState state)
        {
            //Debug.Log("ProductionManager: update needs.");
            var multiplier = (state == ProductionState.on) ? 1 : -1;
            foreach(ResourceUnit<float> unit in needs) _needs[unit.Type] += multiplier * unit.Amount;

        }

        public void UpdateResourcesAmount(ResourceUnit<float>[] needs, ProductionState state)
        {            
            var multiplier = (state == ProductionState.on) ? 1 : -1;
            foreach (ResourceUnit<float> unit in needs)
            {                
                _resourcesBalance[unit.Type] += multiplier * unit.Amount;
                //Debug.Log($"ProductionManager: update {unit.Type} amount to {_resourcesBalance[unit.Type]}");
            }

            _signalBus.Fire(new ResourcesBalanceUpdatedSignal { ResourcesBalance = _resourcesBalance });
        }

        public bool HasResources(ResourceUnit<float>[] needs)
        {
            //Debug.Log("ProductionManager: Check resources.");
            foreach (ResourceUnit<float> unit in needs)
            {
                if (_resourcesBalance[unit.Type] < unit.Amount)
                {
                    //Debug.Log("No resources for " + unit.Type);
                    return false;
                }
            }
            
                
            return true;
        }

    }
}
