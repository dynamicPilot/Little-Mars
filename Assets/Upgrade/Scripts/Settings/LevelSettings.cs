using LittleMars.Common;
using LittleMars.Map;
using LittleMars.Model;
using System;
using UnityEngine;
using Zenject;

namespace LittleMars.Settings
{
    [CreateAssetMenu(menuName = "LittleMars/Level Settings")]
    public class LevelSettings : ScriptableObjectInstaller<LevelSettings>
    {
        public LevelConditionsSettings Conditions;
        public MapSettings Map;
        public LevelGoalsSettings Goals;

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

        [Serializable]
        public class LevelGoalsSettings
        {
            public GoalsManager.Settings GoalsSettings;
        }
        public override void InstallBindings()
        {
            Container.BindInstance(Conditions.InitialConditions);
            Container.BindInstance(Map.Lines);
            Container.BindInstance(Map.Fields);
            Container.BindInstance(Goals.GoalsSettings);
        }
    }
}
