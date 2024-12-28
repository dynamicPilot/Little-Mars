using UnityEngine;
using LittleMars.Animations;
using DG.Tweening;

namespace LittleMars.CameraControls
{
    public class CameraMover : TweenAnimation
    {
        [SerializeField] Transform _transform;
        public void Move(Vector2 delta, float duration)
        {
            var endPosition = _transform.position + new Vector3 (delta.x, delta.y, 0f);
            DoMove(endPosition, duration);
        }

        void DoMove(Vector3 endPosition, float duration)
        {
            _transform.DOMove(endPosition, duration).SetEase(Ease.InOutSine);
        }
    }
}
