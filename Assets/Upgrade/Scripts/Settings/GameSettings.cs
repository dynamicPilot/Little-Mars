using LittleMars.Buildings.Timers;
using LittleMars.Common.Catalogues;
using LittleMars.Installers;
using LittleMars.LevelMenus;
using LittleMars.Localization;
using LittleMars.Model.TimeUpdate;
using LittleMars.Notifications;
using LittleMars.Slots;
using LittleMars.UI.ResourceSlots;
using System;
using UnityEngine;
using Zenject;

namespace LittleMars.Settings
{
    [CreateAssetMenu(menuName = "LittleMars/Settings/Game Settings")]
    public class GameSettings : ScriptableObjectInstaller<GameSettings>
    {
        public ViewSettings View;
        public TimeSettings Time;
        public CatalogueSettings Catalogue;
        public InfoTextSettings InfoTexts;
        

        [Serializable]
        public class ViewSettings
        {
            public GameSceneInstaller.Settings SlotPrefab;
            public ViewSlotFactory.Settings SpawnerSettings;
            public ResourceLimiter.Settings ResourcesLimits; 
        }

        [Serializable]
        public class TimeSettings
        {
            public TimeManager.Settings ManagerSettings;
            public TimeUpdaterTickable.Settings UpdaterSettings;
            public BuildingTimer.Settings DomeTimer;
            public LevelMenusWorkflowTimer.Settings EndGameDelay;
            public LevelNotificationManager.Settings NotSettings;
        }

        [Serializable]
        public class CatalogueSettings
        {
            public IconsCatalogue.Settings Catalogue;
            public ColorsCatalogue.Settings Colors;            
        }

        [Serializable]
        public class InfoTextSettings
        {
            public InfoLangManager.Settings InfoTexts;
        }


        public override void InstallBindings()
        {
            Container.BindInstance(View.SlotPrefab);
            Container.BindInstance(View.SpawnerSettings);
            Container.BindInstance(View.ResourcesLimits);

            Container.BindInstance(Catalogue.Catalogue);
            Container.BindInstance(Catalogue.Colors);

            Container.BindInstance(Time.ManagerSettings);
            Container.BindInstance(Time.UpdaterSettings);
            Container.BindInstance(Time.DomeTimer);
            Container.BindInstance(Time.EndGameDelay);
            Container.BindInstance(Time.NotSettings);

            Container.BindInstance(InfoTexts.InfoTexts);
        }
    }
}
