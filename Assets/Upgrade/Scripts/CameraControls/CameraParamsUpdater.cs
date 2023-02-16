using UnityEngine;

namespace LittleMars.CameraControls
{
    public class CameraParamsUpdater
    {
        readonly Camera _camera;

        CameraParams _params;
        public CameraParamsUpdater()
        {
            _camera = Camera.main;
            _params = new CameraParams(_camera.aspect);
            UpdateCameraParams();
        }

        public CameraParams GetCameraParams()
        {
            UpdateCameraParams();
            return _params;
        }

        void UpdateCameraParams()
        {
            if (_params.Size == _camera.orthographicSize) return;
            _params.UpdateSize(_camera.orthographicSize);
        }
    }
}
