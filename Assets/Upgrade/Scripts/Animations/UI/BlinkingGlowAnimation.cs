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

        string _id = "blinkingGlow";

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
                .SetLoops(-1)
                .SetId(_id);
        }

        public void Play()
        {
            DOTween.Play(_id);
        }

        public void Pause()
        {
            DOTween.Pause(_id);
        }
    }
}
