using DG.Tweening;
//using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace LittleMars.Animations
{
    /// <summary>
    /// Animation. Move RectTransform with Image from startPosition to endPosition with delays.
    /// </summary>
    public class RectMoveAnimation : TweenAnimationUI
    {
        [SerializeField] RectTransform _transform;
        [SerializeField] Image _image;
        [Header("Settings")]
        [SerializeField] Vector3 _startPosition;
        [SerializeField] Vector3 _endPosition;
        [SerializeField] float _duration;
        [SerializeField] float _prepandDelay;
        [SerializeField] float _appendDelay;

        [Header("TimeScale Settings")]
        [SerializeField] bool _unscaledTime = false;

        public override void StartAnimation() => DoMove();

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

            if (_unscaledTime) sequence.SetUpdate(_unscaledTime);
            if (_useId) sequence.SetId(_id);
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
