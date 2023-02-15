using DG.Tweening;
//using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace LittleMars.Animations
{
    public class RectMoveAnimation : TweenAnimation
    {
        [SerializeField] RectTransform _transform;
        [SerializeField] Image _image;
        [Header("Settings")]
        [SerializeField] Vector3 _startPosition;
        [SerializeField] Vector3 _endPosition;
        [SerializeField] float _duration;
        [SerializeField] float _prepandDelay;
        [SerializeField] float _appendDelay;

        private void OnEnable() => StartAnimation();

        protected virtual void StartAnimation() => DoMove();

        void DoMove()
        {
            var sequence = DOTween.Sequence();

            sequence
                .PrependInterval(_prepandDelay)
                .PrependCallback(OnPrepandCallback)
                .Append(_transform.DOLocalMove(_endPosition, _duration).SetEase(Ease.InOutSine))
                .AppendCallback(OnAppendCallback)
                .AppendInterval(_appendDelay)
                .SetLoops(-1);
        }

        protected virtual void OnPrepandCallback()
        {
            _image.enabled = true;
            _transform.localPosition = _startPosition;
        }

        protected virtual void OnAppendCallback()
        {
            _image.enabled = false;
            
        }
    }
}
