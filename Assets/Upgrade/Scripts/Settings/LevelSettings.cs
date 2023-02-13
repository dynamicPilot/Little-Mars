using LittleMars.Common.Levels;
using LittleMars.Localization;
using LittleMars.Map;
using LittleMars.Model;
using LittleMars.Rockets;
using System;
using UnityEngine;
using Zenject;

namespace LittleMars.Settings
{
    [CreateAssetMenu(menuName = "LittleMars/Level Settings")]
    public class LevelSettings : ScriptableObjectInstaller<LevelSettings>
    {
        public LevelInfoSettings Info;
        public LevelConditionsSettings Conditions;
        public MapSettings Map;
        public RocketsSettings Rockets;
        public LevelGoalsSettings Goals;
        public TextBlocksSettings TextBlocks;

        [Serializable]
        public class LevelInfoSettings
        {
            public Common.Levels.LevelInfo LevelInfo;
        }

        [Serializable]
        public class LevelConditionsSettings
        {
            public LevelConditions InitialConditions;
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

        [Serializable]
        public class TextBlocksSettings
        {
            public LangsManager.LevelSettings Blocks;
        }

        [Serializable]
        public class RocketsSettings
        {
            public RocketsManager.Settings Rockets;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(Info.LevelInfo);
            Container.BindInstance(Conditions.InitialConditions);
            Container.BindInstance(Map.Lines);
            Container.BindInstance(Map.Fields);
            Container.BindInstance(Goals.GoalsSettings);
            Container.BindInstance(TextBlocks.Blocks);
            Container.BindInstance(Rockets.Rockets);
        }
    }
}
