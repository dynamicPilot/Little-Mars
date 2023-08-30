using LittleMars.Common;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LittleMars.Settings
{
    [CreateAssetMenu(menuName = "LittleMars/Catalogue/ColorsSettings")]
    public class ColorsSettings : ScriptableObject
    {
        [SerializeField] private ConnectionColorSettings _connections;
        [SerializeField] private BStateColorSettings _production;

        Dictionary<int, Color> _bStatesColors = null;
        Dictionary<int, Color> _connectionsColors = null;

        [Serializable]
        public class ConnectionColorSettings
        {
            public Color Dome;  
            public Color SolarPlant;
            public Color SupplyUnit;
            public Color Farm;
            public Color Mine;
            public Color Factory;
            public Color Workshop;
        }

        [Serializable]
        public class BStateColorSettings
        {
            public Color On;
            public Color Off;
            public Color Paused;
            public Color Effected;
        }


        void CreateBStateDictionary()
        {
            _bStatesColors = new Dictionary<int, Color>();

            _bStatesColors.Add((int)BStates.on, _production.On);
            _bStatesColors.Add((int)BStates.off, _production.Off);
            _bStatesColors.Add((int)BStates.paused, _production.Paused);
            _bStatesColors.Add((int)BStates.effected, _production.Effected);
        }

        void CreateConnectionDictionary()
        {
            _connectionsColors = new Dictionary<int, Color>();

            _connectionsColors.Add((int)BuildingType.dome, _connections.Dome);
            _connectionsColors.Add((int)BuildingType.power_plant, _connections.SolarPlant);
            _connectionsColors.Add((int)BuildingType.supply_plant, _connections.SupplyUnit);
            _connectionsColors.Add((int)BuildingType.farm, _connections.Farm);
            _connectionsColors.Add((int)BuildingType.mine, _connections.Mine);
            _connectionsColors.Add((int)BuildingType.factory, _connections.Factory);
            _connectionsColors.Add((int)BuildingType.workshop, _connections.Workshop);
        }

        public Color GetColor(int index, ColorType type)
        {
            if (type == ColorType.bState)
            {
                if (_bStatesColors == null) CreateBStateDictionary();
                return GetColorFromDict(index, _bStatesColors);
            }
            else if (type == ColorType.connection)
            {
                if (_connectionsColors == null) CreateConnectionDictionary();
                return GetColorFromDict(index, _connectionsColors);
            }
            else
            {
                return Color.red;
            }
        }

        private Color GetColorFromDict(int index, Dictionary<int, Color> dict)
        {
            dict.TryGetValue(index, out var color);
            return color;
        }

    }
}
