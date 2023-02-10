using LittleMars.Buildings;
using LittleMars.Common.Interfaces;
using LittleMars.Common.Signals;
using LittleMars.Models;
using System;
using System.Collections.Generic;
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

        public void Initialize()
        {
            if (!_settings.HasRockets) return;

            _arrivals = new();
            _departures = new();
            _rockets = new();

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

        private void OnHourlySignal(HourlySignal args)
        {
            // check for arrivals
            // check for departures
        }

        private void OnAddBuilding(AddBuildingSignal args)
        {
            // add if cosmodrome
        }

        private void OnRemoveBuilding(RemoveBuildingSignal args)
        {
            // remove from cosmodromes
        }

        private void RocketArrives()
        {
            // check place
            // place rocket
            // changed cosmodrome state to effected
        }

        private void RocketDepartures()
        {
            // check for departure
            // make trades
            // depart
            // changed cosmodrome state to on
        }

        private bool HasPlaceForRocket(out int index)
        {
            // has the empty workable cosmodrome
            index = 0;
            return false;
        }

        private void PlaceRocket(Rocket rocket, int index)
        {
            // place to cosmodrome
            // add departurTime to departurs
        }

        private void SetArrivals()
        {
            foreach(Rocket rocket in _settings.Rockets)
            {
                if (!_arrivals.ContainsKey(rocket.ArrivedHour))
                    _arrivals.Add(rocket.ArrivedHour, new List<Rocket>());

                _arrivals[rocket.ArrivedHour].Add(rocket);
            }
        }

        private void SetDeparture(Rocket rocket)
        {
            if (!_departures.ContainsKey(rocket.DepartureHour))
                _departures.Add(rocket.DepartureHour, new List<Rocket>());

            _departures[rocket.DepartureHour].Add(rocket);
        }


        [Serializable]
        public class Settings
        {
            public bool HasRockets;
            public Rocket[] Rockets;
        }
    }
}
