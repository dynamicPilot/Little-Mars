using DG.Tweening;
using System.Linq;
using UnityEngine;

namespace LittleMars.Animations
{
    public class RectScaleAnimation : TweenAnimation
    {
        [SerializeField] private RectTransform _transform;

        [Header("Animation Settings")]
        [SerializeField] private float _period = 1f;
        [SerializeField] private Vector3 _minScale;
        [SerializeField] private Vector3 _maxScale;
        
        float _signleMoveDuration;
        private void Start()
        {
            _signleMoveDuration = _period / 4f;
            ToMinScale();
        }

        private void ToMinScale()
        {
            _transform.DOScale(_minScale, _signleMoveDuration).OnComplete(Scaling);
        }
        private void Scaling()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(_transform.DOScale(_maxScale, _signleMoveDuration * 2f))
                .Append(_transform.DOScale(_minScale, _signleMoveDuration * 2f))
                .SetEase(Ease.InOutSine)
                .SetLoops(-1);
        }
    }
}
