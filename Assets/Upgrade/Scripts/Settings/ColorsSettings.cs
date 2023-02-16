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


        private void CreateBStateDictionary()
        {
            _bStatesColors = new Dictionary<int, Color>();

            _bStatesColors.Add((int)BStates.on, _production.On);
            _bStatesColors.Add((int)BStates.off, _production.Off);
            _bStatesColors.Add((int)BStates.paused, _production.Paused);
            _bStatesColors.Add((int)BStates.effected, _production.Effected);
        }

        public Color GetColor(int index, ColorType type)
        {
            if (type == ColorType.bState)
            {
                if (_bStatesColors == null) CreateBStateDictionary();
                return GetColorFromDict(index, _bStatesColors);
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
