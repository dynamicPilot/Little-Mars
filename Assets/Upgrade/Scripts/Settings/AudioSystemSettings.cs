using LittleMars.AudioSystems;
using LittleMars.Common.Catalogues;
using System;
using UnityEngine;
using Zenject;

namespace LittleMars.Settings
{
    [CreateAssetMenu(menuName = "LittleMars/Settings/AudioSystem Settings")]
    public class AudioSystemSettings : ScriptableObjectInstaller<AudioSystemSettings>
    {
        public SoundCatalogueSettings Sounds;
        public SystemSettings System;

        [Serializable]
        public class SoundCatalogueSettings
        {
            public SoundsCatalogue.Settings Sounds;
        }
        [Serializable]
        public class SystemSettings
        {
            public AudioSystem.Settings System;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(Sounds.Sounds);
            Container.BindInstance(System.System);
        }

    }
}
