using LittleMars.Installers;
using System;
using UnityEngine;
using Zenject;

namespace LittleMars.Settings
{
    [CreateAssetMenu(menuName = "LittleMars/Settings/WindowObject Settings")]
    public class WindowObjectSettings : ScriptableObjectInstaller<WindowObjectSettings>
    {
        //public TooltipSettings Tooltips;

        //[Serializable]
        //public class TooltipSettings
        //{
        //    public WindowObjectInstaller.Settings Installer;
        //}

        //public override void InstallBindings()
        //{
        //    Container.BindInstance(Tooltips.Installer);
        //}
    }
}
