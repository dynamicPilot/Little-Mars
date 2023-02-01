using DG.Tweening;
using System.Linq;
using UnityEngine;

namespace LittleMars.Animations
{
    internal class BlinkingIndicatorsAnimation : TweenAnimation
    {
        [SerializeField] private SpriteRenderer[] _indicator;
        [SerializeField] private float _period;

        string _id = "blinking";

        private void Start()
        {
            SetAnimations();
        }

        private void SetAnimations()
        {
            for(int i = 0; i < _indicator.Length; i++)
            {
                StartBlinking(_indicator[i]);
            }
        }

        private void StartBlinking(SpriteRenderer indicator)
        {
            Sequence sequence = DOTween.Sequence();

            sequence
                .Append(indicator.DOFade(0f, _period))
                .Append(indicator.DOFade(1f, _period))
                .SetLoops(-1)
                .SetId(_id);
        }
    }
}
