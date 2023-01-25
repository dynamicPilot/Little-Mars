using DG.Tweening;
using UnityEngine;

namespace LittleMars.Animations
{
    public class BlinkedIndicatorAnimation : DOTweenAnimation
    {
        [SerializeField] private SpriteRenderer _indicator;
        [SerializeField] private float _period;

        string _id = "blinking";

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
                .Append(_indicator.DOFade(1f, _period))
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

