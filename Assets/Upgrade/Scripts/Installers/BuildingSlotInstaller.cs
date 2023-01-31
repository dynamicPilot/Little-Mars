using LittleMars.Buildings.View;
using LittleMars.Common;
using LittleMars.UI;
using LittleMars.UI.BuildingSlots;
using LittleMars.UI.BuildingsSlots;
using LittleMars.UI.Effects;
using LittleMars.UI.ResourceSlots;
using LittleMars.UI.SlotUIFactories;
using System;
using UnityEngine;
using Zenject;

namespace LittleMars.Installers
{
    public class BuildingSlotInstaller : MonoInstaller<BuildingSlotInstaller>
    {
        [SerializeField] DraggableItem _draggablePart;

        [Inject][SerializeField] private BuildingType _type;
        [Inject][SerializeField] private Size _size;
        [Inject] Settings _settings;
        public override void InstallBindings()
        {
            Container.BindInstances(_draggablePart);
            Container.BindInstances(_type);
            Container.BindInstances(_size);

            Container.BindInterfacesAndSelfTo<ResourcesScreenController>().AsSingle();
            Container.BindInterfacesAndSelfTo<ConnectionsListUI>().AsSingle();
            Container.BindInterfacesAndSelfTo<FieldsListUI>().AsSingle();

            Container.Bind<ResourceListFactory>().AsSingle();
            Container.Bind<FieldSlotUISetter>().AsCached();
            //Container.Bind<SlotUIFactory<SlotUI>>().AsCached();

            Container.Bind<SlotUIFactory<ResourceSlotUI>>()
                .AsSingle()
                .WithConcreteId(Identifiers.buildingSlot)
                .WhenInjectedInto<ResourceListFactory>();

            Container.Bind<SlotUIFactory<SlotUI>>()
                .WithConcreteId(Identifiers.fieldSlot)               
                .WhenInjectedInto<FieldsListUI>();

            Container.Bind<SlotUIFactory<SlotUI>>()
                .WithConcreteId(Identifiers.connectionSlot)
                .WhenInjectedInto<ConnectionsListUI>();

            Container.Bind<BuildingObject>()
               .FromScriptableObjectResource(String.Concat(_settings.BuildingObjectFolderPath, _type, "_", _size))
               .AsSingle();

            Container.Bind<ISetSlot>().To<BuildingSlotUISetter>()
                //.WhenInjectedInto<SlotUIFactory<SlotUI>>();
                .When(context => context.ConcreteIdentifier.GetHashCode() == Identifiers.connectionSlot.GetHashCode());

            Container.Bind<ISetSlot>().To<FieldSlotUISetter>()
                .When(context => context.ConcreteIdentifier.GetHashCode() == Identifiers.fieldSlot.GetHashCode());

            Container.BindFactory<ResourceSlotUI, PlaceholderFactory<ResourceSlotUI>>()
                .FromComponentInNewPrefab(_settings.ResourceListSlotPrefab)
                .WithGameObjectName("ResourceSlot")
                .When(context => context.ConcreteIdentifier.GetHashCode() == Identifiers.buildingSlot.GetHashCode());

            Container.BindFactory<SlotUI, PlaceholderFactory<SlotUI>>()
                .FromComponentInNewPrefab(_settings.ConnectionListSlotPrefab)
                .WithGameObjectName("ConnectionSlot");
        }

        [Serializable]
        public class Settings
        {
            public string BuildingObjectFolderPath;
            public GameObject ResourceListSlotPrefab;
            public GameObject ConnectionListSlotPrefab;
        }
    }
}
