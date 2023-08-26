using LittleMars.Installers;
using System;
using UnityEngine;
using Zenject;

namespace LittleMars.Settings
{
    [CreateAssetMenu(menuName = "LittleMars/Settings/Window Settings")]
    public class GameWindowSettings: ScriptableObjectInstaller<GameWindowSettings>
    {
        public ResourceFolders Folders;

        [Serializable]
        public class ResourceFolders
        {
            public GameWindowInstaller.Settings Installer;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(Folders.Installer);
        }
    }
}
