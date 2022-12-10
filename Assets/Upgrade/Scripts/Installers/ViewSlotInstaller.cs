using LittleMars.Common;
using LittleMars.Slots;
using LittleMars.Slots.States;
using LittleMars.Slots.UI;
using LittleMars.Slots.Views;
using LittleMars.View;
using System;
using UnityEngine;
using Zenject;

namespace LittleMars.Installers
{
    public class ViewSlotInstaller : MonoInstaller
    {
        [SerializeField] DroppableItem _droppableItem;

        [Inject][SerializeField] Vector2 _position;
        [Inject] Settings _settings;
        public override void InstallBindings()
        {
            Container.Bind<ViewSlotStateManager>().AsSingle();           
            Container.Bind<ViewSlot>().AsSingle();

            Container.BindInterfacesAndSelfTo<ViewSlotState>().AsSingle();

            //Container.BindInstance(_view);
            //Container.BindInstance(_viewUI);
            Container.BindInstance(_position);
            Container.BindInstance(_droppableItem);

            Container.Bind<SlotStateFactory>().AsSingle();

            Container.BindFactory<SlotEmptyState, SlotEmptyState.Factory>().WhenInjectedInto<SlotStateFactory>();
            Container.BindFactory<SlotBuildingState, SlotBuildingState.Factory>().WhenInjectedInto<SlotStateFactory>();
            Container.BindFactory<SlotWaitingState, SlotWaitingState.Factory >().WhenInjectedInto<SlotStateFactory>();
            Container.BindFactory<SlotPlacingState, SlotPlacingState.Factory>().WhenInjectedInto<SlotStateFactory>();


            Container.BindFactory<SignImage, SignImage.Factory>()
                .FromComponentInNewPrefab(_settings.SignImagePrefab)
                .WithGameObjectName("Sign");
            //Container.Bind<EmptySlotViewState>().AsSingle();
            //Container.Bind<IViewState>().To<EmptySlotViewState>().WhenInjectedInto<SlotEmptyState>();
            // bindInstance -> viewSlotView;
        }

        [Serializable]
        public class Settings
        {
            public GameObject SignImagePrefab;
        }
    }
}

