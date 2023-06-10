using LittleMars.Installers;
using System;
using UnityEngine;
using Zenject;


namespace Assets.Upgrade.Scripts.Settings
{
    [CreateAssetMenu(menuName = "LittleMars/Settings/MainMenu Settings")]
    public class MainMenuSettings : ScriptableObjectInstaller<MainMenuSettings>
    {
        public WindowsSettings Windows;

        [Serializable]
        public class WindowsSettings
        {
            public MenuSceneInstaller.Settings SceneSettings;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(Windows.SceneSettings);
        }
    }
}
