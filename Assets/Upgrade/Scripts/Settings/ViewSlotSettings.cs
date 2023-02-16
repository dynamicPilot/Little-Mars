using LittleMars.Installers;
using System;
using UnityEngine;
using Zenject;

namespace LittleMars.Settings
{
    [CreateAssetMenu(menuName = "LittleMars/Settings/ViewSlot Settings")]
    public class ViewSlotSettings: ScriptableObjectInstaller<ViewSlotSettings>
    {
        public ViewSettings View;

        [Serializable]
        public class ViewSettings
        {
            public ViewSlotInstaller.Settings SignImagePrefab;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(View.SignImagePrefab);
        }
    }
}
