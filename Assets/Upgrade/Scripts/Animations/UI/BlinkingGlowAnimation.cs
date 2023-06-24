using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace LittleMars.Animations.UI
{
    public class BlinkingGlowAnimation : TweenAnimation
    {
        [SerializeField] private Image _indicator;
        [SerializeField] private float _endFade;
        [SerializeField] private float _period;

        [Header("Animation ID")]
        [SerializeField] private bool _needSetId = true;
        [SerializeField] string _id = "blinkingGlow";

        [Header("TimeScale Settings")]
        [SerializeField] bool _unscaledTime = false;

        private void Start()
        {
            StartAnimation();
        }

        public void StartAnimation()
        {
            Blinking();
        }

        private void Blinking()
        {
            Sequence sequence = DOTween.Sequence();

            sequence
                .Append(_indicator.DOFade(0f, _period))
                .Append(_indicator.DOFade(_endFade, _period))
                .Append(_indicator.DOFade(0f, _period))
                .SetLoops(-1);

            if (_unscaledTime) sequence.SetUpdate(_unscaledTime);
            if (_needSetId) sequence.SetId(_id);
        }

        public void Play()
        {
            if (_needSetId) DOTween.Play(_id);
        }

        public void Pause()
        {
            if (_needSetId) DOTween.Pause(_id);
        }

        public void OnDestroy()
        {
            DOTween.KillAll();
        }
    }
}
