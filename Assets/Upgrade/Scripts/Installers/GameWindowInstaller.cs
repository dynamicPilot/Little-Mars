using LittleMars.Buildings.View;
using LittleMars.UI.Windows;
using LittleMars.WindowManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace LittleMars.Installers
{
    public class GameWindowInstaller : MonoInstaller<GameWindowInstaller>
    {
        [Inject] WindowID _id;
        [Inject] Settings _settings;
        public override void InstallBindings()
        {
            Container.BindInstance(_id).AsSingle();

            Container.BindFactory<WindowObject, WindowObject.Factory>()
                .FromComponentInNewPrefabResource(String.Concat(_settings.PrefabFolderPath, _id))
                .WithGameObjectName(_id.ToString());
        }

        [Serializable]
        public class Settings
        {
            public string PrefabFolderPath;
        }
    }
}
