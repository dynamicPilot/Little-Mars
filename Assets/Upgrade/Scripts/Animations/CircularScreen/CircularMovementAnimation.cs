using DG.Tweening;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.Animations.CircularScreen
{
    public class CircularMovementAnimation : TweenAnimation
    {
        [SerializeField] RectTransform _transform;
        [SerializeField] Image _image;
        [Header("Settings")]
        [SerializeField] Vector3 _upperPosition;
        [SerializeField] Vector3 _downPosition;
        [SerializeField] float _duration;

        private void OnEnable()
        {
            StartMove();
        }

        void StartMove()
        {
            if (!NeedPrepandMove())
                DoCircularMove();
            else
            {
                Debug.Log("Need Prepand move");
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
        }
        void DoPrepandMove(float duration)
        {
            var sequence = DOTween.Sequence();
            sequence.Append(_transform.DOLocalMove(_upperPosition, duration).SetEase(Ease.Linear))
                .AppendCallback(OnAppendCallback)
                .OnComplete(DoCircularMove);
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
