using DG.Tweening;
using System.Linq;
using UnityEngine;

namespace LittleMars.Animations
{
    public class BlinkingLightAnimation : TweenAnimation
    {
        [SerializeField] private Transform _lineLight;
        [SerializeField] private SpriteRenderer _light0;
        [SerializeField] private SpriteRenderer _light1;

        [Header("Settings")]
        [SerializeField] private float _period;
        [SerializeField] private float _pauseDuration;
        [SerializeField] private float _burstDuration;
        [SerializeField] private float _endPosY;
        
        SpriteRenderer _lineRenderer;
        [SerializeField] Vector3 _initialPos;
        float _duration;        

        private void Start()
        {
            SetAnimation();
        }
        public void Play()
        {
            DOTween.Play(this);
        }

        public void Pause()
        {
            DOTween.Pause(this);
        }

        public void SetAnimation()
        {
            PrepaireParams();
            Blinking();
        }

        private void PrepaireParams()
        {
            _lineRenderer = _lineLight.GetComponent<SpriteRenderer>();
            _initialPos = _lineLight.localPosition;
            _duration = _burstDuration / 4;
        }

        private void Blinking()
        {
            Sequence sequence = DOTween.Sequence();

            sequence.PrependCallback(BeforeCallback)
                .Append(_lineLight.DOLocalMoveY(_endPosY, _period));

            sequence.AppendCallback(AfterCallback)
                .Append(_light0.DOFade(1f, _duration))
                .Append(_light0.DOFade(0f, _duration))
                .Append(_light1.DOFade(1f, _duration))
                .Append(_light1.DOFade(0f, _duration))
                .AppendInterval(_pauseDuration)
                .SetLoops(-1);
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
