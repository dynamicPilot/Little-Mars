using LittleMars.Common;
using LittleMars.UI.BuildingsSlots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            Container.Bind<BuildingObject>()
               .FromScriptableObjectResource(String.Concat(_settings.BuildingObjectFolderPath, _type, "_", _size))
               .AsSingle();
        }

        [Serializable]
        public class Settings
        {
            public string BuildingObjectFolderPath;
        }
    }
}
