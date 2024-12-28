using DG.Tweening;
using LittleMars.Animations;
using UnityEngine;

namespace LittleMars.CameraControls
{
    public class CameraZoomer : TweenAnimation
    {
        Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }
        public void Zoom (float endSize, float duration)
        {
            DoZoom(endSize, duration);
        }

        void DoZoom(float endSize, float duration)
        {
            Debug.Log($"DoZoom : endSize {endSize}");
            _camera.DOOrthoSize(endSize, duration);
        }
    }
}
