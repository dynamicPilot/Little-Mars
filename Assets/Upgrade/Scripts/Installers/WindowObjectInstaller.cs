using LittleMars.Common.Signals;
using LittleMars.Signals;
using LittleMars.TooltipSystem;
using LittleMars.UI.Tooltip;
using LittleMars.UI.Windows;
using System;
using UnityEngine;
using Zenject;

namespace LittleMars.Installers
{
    public class WindowObjectInstaller : MonoInstaller<WindowObjectInstaller>
    {
        [Inject] Settings _settings;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<TooltipManager>().AsSingle();
            Container.Bind<TooltipController>().AsSingle();

            Container.BindFactory<TooltipObject, TooltipObject.Factory>()
                .FromComponentInNewPrefab(_settings.TooltipPrefab)
                .WithGameObjectName("Tooltip");

            var windowRect = GetComponent<RectTransform>();
            Container.BindInstance(windowRect);
        }


        [Serializable]
        public class Settings
        {
            public GameObject TooltipPrefab;
        }
    }
}
