using DG.Tweening;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace LittleMars.Animations.CircularScreen
{
    public class CircularMovementAnimation : TweenAnimationUI
    {
        [SerializeField] RectTransform _transform;
        [SerializeField] Image _image;
        [Header("Settings")]
        [SerializeField] Vector3 _upperPosition;
        [SerializeField] Vector3 _downPosition;
        [SerializeField] float _duration;

        bool _isInAnimation = false;
        public override void StartAnimation() => CheckForStartMoveOrPlay();
        protected override void OnDisable()
        {
            DOTween.Pause(_id);
        }

        void Play() => DOTween.Play(_id);

        void CheckForStartMoveOrPlay()
        {
            if (_isInAnimation) Play();
            else StartMove();
        }
        void StartMove()
        {
            _isInAnimation = true;

            if (!NeedPrepandMove())
                DoCircularMove();
            else
            {
                var duration = GetDurationToUpperPosition();
                DoPrepandMove(duration);
            }
        }

        void DoCircularMove()
        {
            var sequence = DOTween.Sequence();
            sequence.PrependCallback(OnPrepandCallback)
                .Append(_transform.DOLocalMove(_upperPosition, _duration).SetEase(Ease.Linear))
                .AppendCallback(OnAppendCallback)
                .SetLoops(-1);

            if (_useId) sequence.SetId(_id);
        }
        void DoPrepandMove(float duration)
        {
            var sequence = DOTween.Sequence();
            sequence.Append(_transform.DOLocalMove(_upperPosition, duration).SetEase(Ease.Linear))
                .AppendCallback(OnAppendCallback)
                .OnComplete(DoCircularMove);

            if (_useId) sequence.SetId(_id);
        }

        void OnPrepandCallback()
        {
            _transform.localPosition = _downPosition;
            _image.enabled = true;
        }

        void OnAppendCallback()
        {
            _image.enabled = false;
        }

        bool NeedPrepandMove()
        {
            return Mathf.Abs(Vector3.Distance(_transform.localPosition, _downPosition)) > 1;
        }

        float GetDurationToUpperPosition()
        {
            var distance = Vector3.Distance(_transform.localPosition, _upperPosition);
            var totalDistance = Vector3.Distance(_upperPosition, _downPosition);

            return Mathf.Abs(distance / totalDistance * _duration);
        }
    }
}
