using LittleMars.CameraControls;
using LittleMars.Controllers;
using System;
using UnityEngine;
using Zenject;

namespace LittleMars.Settings
{
    [CreateAssetMenu(menuName = "LittleMars/Settings/Input&Camera Settings")]
    public class InputAndCameraSettings : ScriptableObjectInstaller<InputAndCameraSettings>
    {
        public InputSettings Input;
        public CameraSettings Camera;

        [Serializable]
        public class InputSettings
        {
            public InputMoveDetection.Settings MoveDetection;
            public Controllers.InputControl.Settings InputControl;
        }

        [Serializable]
        public class CameraSettings
        {
            public CameraViewPositioner.Settings Borders;
            public ZoomControl.Settings Zoom;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(Input.MoveDetection);
            Container.BindInstance(Input.InputControl);
            Container.BindInstance(Camera.Borders);
            Container.BindInstance(Camera.Zoom);
        }
    }
}
