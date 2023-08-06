using DG.Tweening;
using System.Linq;
using UnityEngine;

namespace LittleMars.Animations
{
    public class BlinkingLightAnimation : TweenAnimationEffect
    {
        [SerializeField] private Transform _lineLight;
        [SerializeField] private SpriteRenderer _light0;
        [SerializeField] private SpriteRenderer _light1;

        [Header("Settings")]
        [SerializeField] private float _period;
        [SerializeField] private float _delayDuration;
        [SerializeField] private float _pauseDuration;
        [SerializeField] private float _burstDuration;
        [SerializeField] private float _endPosY;
        
        SpriteRenderer _lineRenderer;
        Vector3 _initialPos;
        float _duration;
        bool _needDelay = false;

        public override void StartAnimation()
        {
            SetId();
            PrepaireParams();
            Blinking();
        }

        void SetId()
        {
            var suffix = Mathf.CeilToInt(Random.value * 1000);
            _id = string.Concat(_id, "_", suffix.ToString());
        }

        private void PrepaireParams()
        {
            _lineRenderer = _lineLight.GetComponent<SpriteRenderer>();
            _initialPos = _lineLight.localPosition;
            _duration = _burstDuration / 4;

            _needDelay = (_delayDuration > 0f);
        }

        private void Blinking()
        {
            Sequence sequence = DOTween.Sequence();

            if (_needDelay)
                sequence.AppendInterval(_delayDuration);

            sequence.PrependCallback(BeforeCallback)
                .Append(_lineLight.DOLocalMoveY(_endPosY, _period));

            sequence.AppendCallback(AfterCallback)
                .Append(_light0.DOFade(1f, _duration))
                .Append(_light0.DOFade(0f, _duration))
                .Append(_light1.DOFade(1f, _duration))
                .Append(_light1.DOFade(0f, _duration))
                .AppendInterval(_pauseDuration)
                .SetLoops(-1).SetId(_id);
        }

        private void BeforeCallback()
        {
            _lineLight.localPosition = _initialPos;
            _lineRenderer.enabled = true;

            if (_light0.color.a != 0f) 
                _light0.color = new Color(_light0.color.r, _light0.color.g, _light0.color.b, 0f);

            if (_light1.color.a != 0f)
                _light1.color = new Color(_light1.color.r, _light1.color.g, _light1.color.b, 0f);
        }

        private void AfterCallback()
        {
            _lineRenderer.enabled = false;
        }
    }
}
