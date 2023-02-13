using LittleMars.Common;
using LittleMars.Common.Interfaces;
using LittleMars.Common.Signals;
using LittleMars.Models;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Zenject;

namespace LittleMars.Rockets
{
    public class RocketsManager : IInitializable, IDisposable
    {
        readonly Settings _settings;
        readonly SignalBus _signalBus;
        readonly TradeManager _tradeManager;

        Dictionary<int, List<Rocket>> _arrivals = null;
        Dictionary<int, List<Rocket>> _departures = null;

        Dictionary<Rocket, IBuildingFacade> _rockets = null;
        List<IBuildingFacade> _cosmodromes = null;

        public RocketsManager(Settings settings, SignalBus signalBus, TradeManager tradeManager)
        {
            _settings = settings;
            _signalBus = signalBus;
            _tradeManager = tradeManager;
        }

        public void Initialize()
        {
            if (!_settings.HasRockets) return;

            _arrivals = new();
            _departures = new();
            _rockets = new();
            _cosmodromes= new();

            SetArrivals();

            _signalBus.Subscribe<HourlySignal>(OnHourlySignal);
            _signalBus.Subscribe<AddBuildingSignal>(OnAddBuilding);
            _signalBus.Subscribe<RemoveBuildingSignal>(OnRemoveBuilding);
        }

        public void Dispose()
        {
            if (!_settings.HasRockets) return;

            _signalBus?.Unsubscribe<HourlySignal>(OnHourlySignal);
            _signalBus?.Unsubscribe<AddBuildingSignal>(OnAddBuilding);
            _signalBus?.Unsubscribe<RemoveBuildingSignal>(OnRemoveBuilding);
        }

        void OnHourlySignal(HourlySignal args)
        {
            // check for arrivals
            if (_arrivals.ContainsKey(args.Hour))
                _arrivals[args.Hour].ForEach(RocketArrives);

            // check for departures
            if (_departures.ContainsKey(args.Hour))
                _departures[args.Hour].ForEach(RocketDepartures);

            RemoveDeparture(args.Hour);
        }

        void OnAddBuilding(AddBuildingSignal args)
        {
            if (args.BuildingFacade.Info().Type == Common.BuildingType.cosmodrome)
                _cosmodromes.Add(args.BuildingFacade);
        }

        void OnRemoveBuilding(RemoveBuildingSignal args)
        {
            if (args.BuildingFacade.Info().Type == Common.BuildingType.cosmodrome)
                RemoveCosmodrome(args.BuildingFacade);
        }

        void RocketArrives(Rocket rocket)
        {
            var hasPlace = TryGetPlaceForRocket(out IBuildingFacade cosmodrome);
            if (!hasPlace)
            {
                Debug.Log($"RocketManager : No free cosmodromes.");
                return;
            }

            PlaceRocket(rocket, cosmodrome);
            Debug.Log($"Rocket will be placed to cosmodrome.");
            _rockets[rocket].OnStartViewEffect();
        }

        void RocketDepartures(Rocket rocket)
        {
            _tradeManager.MakeTradeByRocket(rocket);
            RemoveRocket(rocket);           
        }

        void RemoveCosmodrome(IBuildingFacade cosmodrome)
        {
            if (!_cosmodromes.Contains(cosmodrome)) return;

            RemoveRocketFromCosmodrome(cosmodrome);
            _cosmodromes.Remove(cosmodrome);
        }

        bool TryGetPlaceForRocket(out IBuildingFacade cosmodrome)
        {
            // has the empty workable cosmodrome
            cosmodrome = null;
            var freeCosmodromes = _cosmodromes?.Except(_rockets?.Values)
                .Where(c => c.State() == States.on).ToList();

            if (freeCosmodromes== null || freeCosmodromes.Count == 0) return false;

            cosmodrome = freeCosmodromes.First();
            return true;
        }

        void PlaceRocket(Rocket rocket, IBuildingFacade cosmodrome)
        {
            _rockets.Add(rocket, cosmodrome);
            AddDeparture(rocket);
        }

        void RemoveRocket(Rocket rocket)
        {
            //RemoveDeparture(rocket);
            _rockets[rocket].OnEndViewEffect();
            _rockets.Remove(rocket);
        }

        void RemoveRocketFromCosmodrome(IBuildingFacade cosmodrome)
        {
            var rocketsToRemove = _rockets.Keys.Where(r => _rockets[r] == cosmodrome);

            foreach (var rocket in rocketsToRemove) 
                RemoveRocket(rocket);
        }

        void SetArrivals()
        {
            foreach(Rocket rocket in _settings.Rockets)
            {
                if (!_arrivals.ContainsKey(rocket.ArrivedHour))
                    _arrivals.Add(rocket.ArrivedHour, new List<Rocket>());

                _arrivals[rocket.ArrivedHour].Add(rocket);
            }
        }

        void AddDeparture(Rocket rocket)
        {
            if (!_departures.ContainsKey(rocket.DepartureHour))
                _departures.Add(rocket.DepartureHour, new List<Rocket>());

            _departures[rocket.DepartureHour].Add(rocket);
        }

        void RemoveDeparture(int hour)
        {
            if (!_departures.ContainsKey(hour)) return;
            //if (!_departures[hour].Contains(rocket)) return;

            _departures[hour].Clear();
        }


        [Serializable]
        public class Settings
        {
            public bool HasRockets;
            public Rocket[] Rockets;
        }
    }
}
