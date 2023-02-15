using DG.Tweening;
using System.Linq;
using UnityEngine;

namespace LittleMars.Animations
{
    public class FlipAnimation : TweenAnimation
    {
        [SerializeField] RectTransform _transform;
        [Header("Settings")]
        [SerializeField] float _duration;
        [SerializeField] Vector3 _startRotate;
        [SerializeField] Vector3 _endRotate;

        public void StartAnimation()
        {
            DoFlip();
        }

        //public void Play()
        //{
        //    DOTween.Play(this);
        //}
        //public void Pause()
        //{
        //    DOTween.Pause(this);
        //}

        void DoFlip()
        {
            var sequence = DOTween.Sequence();
            sequence.PrependInterval(_duration)
                .PrependCallback(OnPrepandCallback)
                .AppendInterval(_duration)
                .AppendCallback(OnAppendCallback)
                .SetLoops(-1);
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
