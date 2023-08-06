using DG.Tweening;
using System.Linq;
using UnityEngine;

namespace LittleMars.Animations
{
    internal class BlinkingIndicatorsAnimation : TweenAnimationObject
    {
        [SerializeField] private SpriteRenderer[] _indicator;
        [SerializeField] private float _period;
        //[SerializeField] string _id = "blinking";

        public void AddToId(string suffix)
        {
            string.Concat(_id, "_", suffix);
            StartAnimation();
        }

        public override void StartAnimation()
        {
            base.StartAnimation();

            var suffix = Mathf.CeilToInt(Random.value * 1000);
            _id = string.Concat(_id, "_", suffix.ToString(),"_");

            for (int i = 0; i < _indicator.Length; i++)
            {
                StartBlinking(_indicator[i], i);
            }
        }

        void StartBlinking(SpriteRenderer indicator, int index)
        {
            Sequence sequence = DOTween.Sequence();
            var id = string.Concat(_id, index.ToString());
            _ids.Add(id);

            sequence
                .Append(indicator.DOFade(0f, _period))
                .Append(indicator.DOFade(1f, _period))
                .SetLoops(-1).SetId(id);
        }
    }
}
