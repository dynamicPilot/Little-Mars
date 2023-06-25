using DG.Tweening;
using UnityEngine;

namespace LittleMars.Animations
{
    public class FlipAnimation : TweenAnimationUI
    {
        [SerializeField] RectTransform _transform;
        [Header("Settings")]
        [SerializeField] float _duration;
        [SerializeField] Vector3 _startRotate;
        [SerializeField] Vector3 _endRotate;
        public override void StartAnimation() => DoFlip();

        void DoFlip()
        {
            var sequence = DOTween.Sequence();
            sequence.PrependInterval(_duration)
                .PrependCallback(OnPrepandCallback)
                .AppendInterval(_duration)
                .AppendCallback(OnAppendCallback)
                .SetLoops(-1);

            if (_useId) sequence.SetId(_id);
        }

        void OnPrepandCallback()
        {
            _transform.Rotate(_endRotate);
        }

        void OnAppendCallback()
        {
            _transform.Rotate(_startRotate);
        }
    }
}
