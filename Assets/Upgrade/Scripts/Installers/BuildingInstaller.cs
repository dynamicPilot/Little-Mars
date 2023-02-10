using System;
using Zenject;
using UnityEngine;
using LittleMars.Buildings.BuildingStates;
using LittleMars.Common;
using LittleMars.Buildings.Parts;
using LittleMars.Buildings.View;
using LittleMars.Buildings;
using LittleMars.Buildings.Timers;
using LittleMars.Buildings.View.States;

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

            Container.Bind<BuildingOnStateFactory>().AsSingle();
            Container.Bind<BuildingOffStateFactory>().AsSingle();
            Container.Bind<BuildingPausedStateFactory>().AsSingle();

            Container.BindInterfacesAndSelfTo<BuildingTimer>().AsSingle();

            Container.BindFactory<BuildingOnState, BuildingOnState.Factory>().AsSingle();
            Container.BindFactory<BuildingOffState, BuildingOffState.Factory>().AsSingle();
            Container.BindFactory<BuildingPausedState, BuildingPausedState.Factory>().AsSingle();

            Container.BindFactory<DomeOnState, DomeOnState.Factory>().AsSingle();
            Container.BindFactory<DomeOffState, DomeOffState.Factory>().AsSingle();
            Container.BindFactory<DomePausedState, DomePausedState.Factory>().AsSingle();

            Container.BindInstance(_type).AsSingle();
            Container.BindInstance(_size).AsSingle();

            Container.Bind<BuildingObject>()
                .FromScriptableObjectResource(String.Concat(_settings.BuildingObjectFolderPath, _type, "_", _size))
                .AsSingle();

            Container.BindFactory<BuildingObjectViewFacade, BuildingObjectViewFacade.Factory>()
                .FromComponentInNewPrefabResource(String.Concat(_settings.PrefabFolderPath, _type, "_", _size))
                .WithGameObjectName("View");
        }


        //private void InstallStates()
        //{
        //    Container.BindFactory<BuildingOnState, BuildingOnState.Factory>().FromFactory<BuildingOnStateFactory>();
        //    Container.BindFactory<BuildingOffState, OffStateFactory>().FromFactory<BuildingOffStateFactory>();
        //    Container.BindFactory<BuildingPausedState, PausedStateFactory>().FromFactory<BuildingPausedStateFactory>();
        //}

        [Serializable]
        public class Settings
        {
            public string PrefabFolderPath;
            public string BuildingObjectFolderPath;
        }
    }
}
