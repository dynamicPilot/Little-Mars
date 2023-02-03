using LittleMars.Common;
using LittleMars.Common.Levels;
using LittleMars.Common.Signals;
using LittleMars.Model.Interfaces;
using System;
using System.Collections.Generic;
using Zenject;
using UnityEngine;

namespace LittleMars.Models
{
    public class ProductionManager : IInitializable, IDisposable
    {
        Dictionary<Resource, float> _needs = null;
        Dictionary<Resource, Dictionary<Period, float>> _production = null;
        Dictionary<Resource, float> _resourcesBalance = null;
        Dictionary<Resource, float> _totalProduction = null;

        ProductionHelper _helper;
        ResourcesBalancer _balancer;
        LevelConditions _settings;
        SignalBus _signalBus;

        Period _period;

        ResourcesBalanceUpdatedSignal _balanceSignal;
        ResourcesProductionChangedSignal _productionSignal;
        ResourcesNeedsChangedSignal _needsSignal;

        int _count = 0;
        public ProductionManager(ProductionHelper helper, 
            LevelConditions settings, SignalBus signalBus, 
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
            _helper.FillResourceDict(out _totalProduction);
            _helper.FillResourceDict(out _needs);
            _helper.FillResourceDictWithResourceUnits(_settings.Resources, out _resourcesBalance);

            _signalBus.Subscribe<PeriodChangeSignal>(OnPeriodChanged);
            _signalBus.Subscribe<HourlySignal>(UpdateBalance);

            // signals
            _balanceSignal = new ResourcesBalanceUpdatedSignal { ResourcesBalance = _resourcesBalance };
            _productionSignal = new ResourcesProductionChangedSignal
            {
                Period = _period,
                Production = _production
            };
            _needsSignal = new ResourcesNeedsChangedSignal { Needs = _needs };

            //Debug.Log("End init");
            OnBalanceChanged();
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

            OnProductionChnaged();
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
                    _totalProduction[(Resource)i] += _production[resource][_period];
                    //Debug.Log($" Update {resource}. After {_resourcesBalance[resource]}");
                }
            }
            UpdateResourcesAmount(units, States.on);
        }

        private void BalanceResource(Resource resource)
        {
            _count++;
            _balancer.HelpBalanceResource(resource);
            UpdateBalance();

            if (_count > 100)
            {
                //Debug.Log("ProductionManager: FORCE STOP");
                return;
            }
        }

        public void UpdateProduction(Dictionary<Resource, Dictionary<Period, float>> production, States state)
        {
            //Debug.Log("ProductionManager: update production. State " + state);
            var multiplier = (state == States.on) ? 1 : -1;
            foreach (Resource resource in production.Keys)
            {
                foreach (Period period in production[resource].Keys)
                    _production[resource][period] += multiplier * production[resource][period];
            }

            OnProductionChnaged();

        }

        public void UpdateNeeds(ResourceUnit<float>[] needs, States state)
        {
            //Debug.Log("ProductionManager: update needs.");
            var multiplier = (state == States.on) ? 1 : -1;
            foreach(ResourceUnit<float> unit in needs) _needs[unit.Type] += multiplier * unit.Amount;

            _signalBus.Fire(_needsSignal);

        }

        public void UpdateResourcesAmount(ResourceUnit<float>[] needs, States state)
        {            
            var multiplier = (state == States.on) ? 1 : -1;
            foreach (ResourceUnit<float> unit in needs)
            {                
                _resourcesBalance[unit.Type] += multiplier * unit.Amount;
                //Debug.Log($"ProductionManager: update {unit.Type} amount to {_resourcesBalance[unit.Type]}");
            }

            OnBalanceChanged();
        }

        private void OnProductionChnaged()
        {
            _productionSignal.Period = _period;
            _signalBus.Fire(_productionSignal);
        }

        private void OnBalanceChanged()
        {
            _signalBus.Fire(_balanceSignal);
        }

        public bool HasResources(ResourceUnit<float>[] needs)
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

        public bool HasResourcesForNeeds(ResourceUnit<float>[] needs)
        {
            Debug.Log("ProductionManager: Check resources for need.");
            foreach (ResourceUnit<float> unit in needs)
            {
                if (_resourcesBalance[unit.Type] + _production[unit.Type][_period] - _needs[unit.Type] < unit.Amount)
                {
                    Debug.Log("No resources for " + unit.Type);
                    return false;
                }
            }

            return true;
        }

    }
}
