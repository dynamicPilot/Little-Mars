using DG.Tweening;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace LittleMars.Animations.UI
{
    public class WindowAnimations : TweenAnimationUI
    {
        [SerializeField] float _duration = 0.25f;
        [SerializeField] float _maxScale = 1.02f;

        [Header("TimeScale Settings")]
        [SerializeField] bool _unscaledTime = false;

        RectTransform _transform;
        float _endScale = 1f;

        private void Awake()
        {
            _transform = GetComponent<RectTransform>();
            _transform.localScale = Vector3.zero;
        }

        public override void StartAnimation() => Open();

        private void Open()
        {
            Sequence sequence = DOTween.Sequence();

            sequence
                .Append(_transform.DOScale(_maxScale, _duration / 3f))
                .Append(_transform.DOScale(_endScale, _duration * 2f / 3f))
                .SetEase(Ease.InOutSine);

            if (_unscaledTime) sequence.SetUpdate(_unscaledTime);
            if (_useId) sequence.SetId(_id);
        }

        private void Close()
        {
            Sequence sequence = DOTween.Sequence();

            sequence
                .Append(_transform.DOScale(0f, _duration / 3f))
                .SetEase(Ease.InOutSine);

            if (_unscaledTime) sequence.SetUpdate(_unscaledTime);
            if (_useId) sequence.SetId(_id);
        }
    }
}

