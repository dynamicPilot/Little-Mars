using System;
using Zenject;
using UnityEngine;
using LittleMars.Buildings.BuildingStates;
using LittleMars.Common;
using LittleMars.Buildings.Parts;
using LittleMars.Buildings.View;
using LittleMars.Buildings;

namespace LittleMars.Installers
{
    public class BuildingInstaller : MonoInstaller<BuildingInstaller>
    {

        [Inject] [SerializeField] private BuildingType _type;
        [Inject] [SerializeField] private Size _size;

        [Inject] Settings _settings;
        public override void InstallBindings()
        {
            Container.Bind<BuildingStateManager>().AsSingle();
            Container.Bind<BuildingOperation>().AsSingle();
            Container.Bind<BuildingData>().AsSingle();
            Container.Bind<BuildingState>().AsSingle();

            Container.BindFactory<BuildingOnState, BuildingOnState.Factory>().WhenInjectedInto<BuildingStateManager>();
            Container.BindFactory<BuildingOffState, BuildingOffState.Factory>().WhenInjectedInto<BuildingStateManager>();
            Container.BindFactory<BuildingPausedState, BuildingPausedState.Factory>().WhenInjectedInto<BuildingStateManager>();

            Container.BindInstance(_type).AsSingle();
            Container.BindInstance(_size).AsSingle();

            Container.Bind<BuildingObject>()
                .FromScriptableObjectResource(String.Concat(_settings.BuildingObjectFolderPath, _type, "_", _size))
                .AsSingle();

            Container.BindFactory<BuildingObjectView, BuildingObjectView.Factory>()
                .FromComponentInNewPrefabResource(String.Concat(_settings.PrefabFolderPath, _type, "_", _size))
                .WithGameObjectName("View");
        }

        [Serializable]
        public class Settings
        {
            public string PrefabFolderPath;
            public string BuildingObjectFolderPath;
        }
    }
}
