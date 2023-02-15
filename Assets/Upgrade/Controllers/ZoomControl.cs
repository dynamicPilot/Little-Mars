using LittleMars.CameraControls;
using System;
using Zenject;

namespace LittleMars.Controllers
{
    public class ZoomControl : IInitializable
    {
        readonly Settings _settings;
        readonly CameraZoomer _zoomer;
        readonly CameraParamsUpdater _paramsUpdater;

        float _size;

        public ZoomControl(Settings settings, CameraZoomer zoomer, CameraParamsUpdater paramsUpdater)
        {
            _settings = settings;
            _zoomer = zoomer;
            _paramsUpdater = paramsUpdater;
        }

        public void Initialize()
        {
            _size = _paramsUpdater.GetCameraParams().Size;
        }

        public void ZoomIn() => ClarifyZoom(-1);

        public void ZoomOut() => ClarifyZoom(1);

        void ClarifyZoom(int multiplier)
        {
            var newSize = _size + multiplier * _settings.ZoomStep;
            if (newSize >= _settings.MinSize && newSize <= _settings.MaxSize)
            {                
                _size = newSize;
                Zoom();
            }
        }

        void Zoom()
        {
            _zoomer.Zoom(_size, _settings.ZoomDuration);
        }


        [Serializable]
        public class Settings
        {
            public float ZoomStep;
            public float MinSize;
            public float MaxSize;
            public float ZoomDuration;
        }
    }
}
