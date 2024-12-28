using System;
using UnityEngine;

namespace LittleMars.CameraControls
{
    public class CameraViewPositioner
    {
        readonly CameraParamsUpdater _paramsUpdater;
        readonly Transform _cameraTransform;
        readonly Settings _settings;

        CameraParams _params;

        public CameraViewPositioner(CameraParamsUpdater paramsUpdater, Settings settings)
        {
            _paramsUpdater = paramsUpdater;
            _settings = settings;
            _cameraTransform = Camera.main.transform;

            _params = null;
        }

        public void UpdateParams()
        {
            _params = _paramsUpdater.GetCameraParams();
        }

        public Vector2 GetClarifiedMovingDelta(Vector2 initialDelta)
        {
            var delta = initialDelta;
            var cameraPosition
                = new Vector2(_cameraTransform.position.x + delta.x, _cameraTransform.position.y + delta.y);

            if (cameraPosition.x - _params.Width / 2 < _settings.LeftBorder)
                delta.x = _settings.LeftBorder + _params.Width / 2 - cameraPosition.x;

            if (cameraPosition.x + _params.Width / 2 > _settings.RightBorder)
                delta.x = _settings.RightBorder - (_cameraTransform.position.x + _params.Width / 2);

            if (cameraPosition.y + _params.Height / 2 > _settings.UpBorder)
                delta.y = _settings.UpBorder - (_cameraTransform.position.y + _params.Height / 2);

            if (cameraPosition.y - _params.Height / 2 < _settings.DownBorder)
                delta.y = _settings.DownBorder - _cameraTransform.position.y + _params.Height / 2;

            return delta;
        }

        [Serializable]
        public class Settings
        {
            public float UpBorder;
            public float RightBorder;
            public float DownBorder;
            public float LeftBorder;
        }
    }
}
