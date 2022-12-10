using LittleMars.Buildings;
using LittleMars.Common;
using LittleMars.Installers;
using LittleMars.Slots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace LittleMars.Settings
{
    [CreateAssetMenu(menuName = "LittleMars/Game Settings")]
    public class GameSettings : ScriptableObjectInstaller<GameSettings>
    {
        public ViewSettings View;
        public CatalogueSettings Catalogue;

        [Serializable]
        public class ViewSettings
        {
            public GameSceneInstaller.Settings SlotPrefab;
            public ViewSlotFactory.Settings SpawnerSettings;
        }

        [Serializable]

        public class CatalogueSettings
        {
            public BuildingCatalogue.Settings Catalogue;
        }


        public override void InstallBindings()
        {
            Container.BindInstance(View.SlotPrefab);
            Container.BindInstance(View.SpawnerSettings);
            Container.BindInstance(Catalogue.Catalogue);
        }
    }
}
