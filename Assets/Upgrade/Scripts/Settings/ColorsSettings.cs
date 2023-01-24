using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LittleMars.Settings
{
    [CreateAssetMenu(menuName = "LittleMars/ColorsSettings")]
    public class ColorsSettings : ScriptableObject
    {
        [SerializeField] private ConnectionColorSettings _connections;

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


    }
}
