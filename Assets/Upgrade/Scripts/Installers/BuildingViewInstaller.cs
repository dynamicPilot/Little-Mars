using LittleMars.Buildings.View;
using LittleMars.Buildings.View.States;
using UnityEngine;
using Zenject;

namespace LittleMars.Installers
{
    public class BuildingViewInstaller : MonoInstaller<BuildingViewInstaller>
    {
        [SerializeField] BuildingObjectViewFacade _facade;
        [SerializeField] BuildingObjectViewComponents _components;

        public override void InstallBindings()
        {
            Container.Bind<BuildingViewStatesManager>().AsSingle();
            Container.Bind<BuildingObjectViewStates>().AsSingle();

            Container.BindInstance(_components);
            Container.BindInstance(_facade);

            Container.BindFactory<BuildingViewOnState, BuildingViewOnState.Factory>().AsSingle();
            Container.BindFactory<BuildingViewOffState, BuildingViewOffState.Factory>().AsSingle();
            Container.BindFactory<BuildingViewPausedState, BuildingViewPausedState.Factory>().AsSingle();
            Container.BindFactory<BuildingViewEffectedState, BuildingViewEffectedState.Factory>().AsSingle();
        }
    }
}
