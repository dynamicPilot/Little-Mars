using LittleMars.Common;
using LittleMars.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace LittleMars.Settings
{
    [CreateAssetMenu(menuName = "LittleMars/Level Settings")]
    public class LevelSettings : ScriptableObjectInstaller<LevelSettings>
    {
        public LevelConditionsSettings Conditions;
        public MapSettings Map;

        [Serializable]
        public class LevelConditionsSettings
        {
            public LevelConditions.Settings InitialConditions;
        }

        [Serializable]
        public class MapSettings
        {
            public MapManager.Settings Lines;
            public FieldManager.Settings Fields;

        }
        public override void InstallBindings()
        {
            Container.BindInstance(Conditions.InitialConditions);
            Container.BindInstance(Map.Lines);
            Container.BindInstance(Map.Fields);
        }
    }
}
