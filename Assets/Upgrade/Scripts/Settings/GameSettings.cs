using LittleMars.Buildings;
using LittleMars.Common;
using LittleMars.Installers;
using LittleMars.Localization;
using LittleMars.Model.TimeUpdate;
using LittleMars.Slots;
using System;
using UnityEngine;
using Zenject;

namespace LittleMars.Settings
{
    [CreateAssetMenu(menuName = "LittleMars/Game Settings")]
    public class GameSettings : ScriptableObjectInstaller<GameSettings>
    {
        public ViewSettings View;
        public TimeSettings Time;
        public CatalogueSettings Catalogue;
        public TextBlocksSettings TextBlocks;

        [Serializable]
        public class ViewSettings
        {
            public GameSceneInstaller.Settings SlotPrefab;
            public ViewSlotFactory.Settings SpawnerSettings;
        }

        [Serializable]
        public class TimeSettings
        {
            public TimeManager.Settings ManagerSettings;
            public TimeUpdater.Settings UpdaterSettings;
        }

        [Serializable]
        public class CatalogueSettings
        {
            public IconsCatalogue.Settings Catalogue;
            public ColorsCatalogue.Settings Colors;
        }

        [Serializable]
        public class TextBlocksSettings
        {
            public LangsManager.SceneSettings Blocks;
        }


        public override void InstallBindings()
        {
            Container.BindInstance(View.SlotPrefab);
            Container.BindInstance(View.SpawnerSettings);
            Container.BindInstance(Catalogue.Catalogue);
            Container.BindInstance(Catalogue.Colors);
            Container.BindInstance(Time.ManagerSettings);
            Container.BindInstance(Time.UpdaterSettings);
            Container.BindInstance(TextBlocks.Blocks);
        }
    }
}
