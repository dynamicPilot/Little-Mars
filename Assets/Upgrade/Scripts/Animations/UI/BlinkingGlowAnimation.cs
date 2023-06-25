using DG.Tweening;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace LittleMars.Animations.UI
{

    public class BlinkingGlowAnimation : TweenAnimationUI
    {
        [SerializeField] Image _indicator;
        [SerializeField] float _endFade;
        [SerializeField] float _period;

        [Header("TimeScale Settings")]
        [SerializeField] bool _unscaledTime = false;

        public override void StartAnimation() => Blinking();
        private void Blinking()
        {
            Sequence sequence = DOTween.Sequence();

            sequence
                .Append(_indicator.DOFade(0f, _period))
                .Append(_indicator.DOFade(_endFade, _period))
                .Append(_indicator.DOFade(0f, _period))
                .SetLoops(-1);

            if (_unscaledTime) sequence.SetUpdate(_unscaledTime);
            if (_useId) sequence.SetId(_id);
        }

        //public void Play()
        //{
        //    if (_useId) DOTween.Play(_id);
        //}

        //public void Pause()
        //{
        //    if (_useId) DOTween.Pause(_id);
        //}
    }
}
