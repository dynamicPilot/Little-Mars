using LittleMars.Installers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using LittleMars.TimescaleControls;

namespace LittleMars.Settings
{
    [CreateAssetMenu(menuName = "LittleMars/Settings/Timescale Settings")]
    public class SceneTimescaleSetting : ScriptableObjectInstaller<SceneTimescaleSetting>
    {
        public TimescaleControl.Settings TimescaleSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(TimescaleSettings);
        }
    }
}
