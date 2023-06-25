using DG.Tweening;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace LittleMars.Animations.UI
{
    public class BlinkingGlowsAnimation : TweenAnimationUI
    {
        [SerializeField] Image[] _indicators;
        [SerializeField] private float _endFade;
        [SerializeField] private float _period;

        [Header("TimeScale Settings")]
        [SerializeField] bool _unscaledTime = false;

        public override void StartAnimation()
        {
            for (int i= 0; i < _indicators.Length;i++) Blinking(i);
        }

        void Blinking(int index)
        {
            Sequence sequence = DOTween.Sequence();

            sequence
                .Append(_indicators[index].DOFade(0f, _period))
                .Append(_indicators[index].DOFade(_endFade, _period))
                .Append(_indicators[index].DOFade(0f, _period))
                .SetLoops(-1);

            if (_unscaledTime) sequence.SetUpdate(_unscaledTime);
            if (_useId) sequence.SetId(String.Concat(_id, index.ToString()));
        }

        protected override void OnDisable()
        {
            if (!_useId) return;
            for (int i = 0; i < _indicators.Length; i++)
                DOTween.Kill(String.Concat(_id, i.ToString()));
        }
    }
}
