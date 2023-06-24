using DG.Tweening;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace LittleMars.Animations.UI
{
    public class BlinkingGlowsAnimation : TweenAnimation
    {
        [SerializeField] Image[] _indicators;
        [SerializeField] private float _endFade;
        [SerializeField] private float _period;

        [Header("Animation ID")]
        [SerializeField] private bool _needSetId = true;
        [SerializeField] string _id = "blinkingGlow_";

        [Header("TimeScale Settings")]
        [SerializeField] bool _unscaledTime = false;

        private void Start()
        {
            StartAnimation();
        }

        public void StartAnimation()
        {
            for (int i= 0; i<_indicators.Length;i++) Blinking(i);
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
            if (_needSetId) sequence.SetId(_id.Concat(index.ToString()));
        }

        public void Play()
        {
            for (int i = 0; i < _indicators.Length; i++)
                if (_needSetId) DOTween.Play(_id.Concat(i.ToString()));
        }

        public void Pause()
        {
            for (int i = 0; i < _indicators.Length; i++)
                if (_needSetId) DOTween.Pause(_id.Concat(i.ToString()));
        }

        public void OnDestroy()
        {
            DOTween.KillAll();
        }
    }
}
