using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace LittleMars.Common
{
    [CreateAssetMenu(menuName = "LittleMars/Catalogue")]
    public class Catalogue : ScriptableObject
    {
        public DomeSettings Dome;
        public MineSettings Mine;

        [Serializable]
        public class DomeSettings
        {
            public BuildingObject Small;

        }

        [Serializable]
        public class MineSettings
        {
            public BuildingObject Small;

        }
    }
}
