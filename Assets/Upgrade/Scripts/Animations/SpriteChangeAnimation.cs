using DG.Tweening;
using UnityEngine;

namespace LittleMars.Animations
{
    public class SpriteChangeAnimation: TweenAnimation
    {
        [SerializeField] SpriteRenderer _renderer;

        [Header("Settings")]
        [SerializeField] bool _isEnableInStart;
        [SerializeField] bool _isEnableInEnd;
        [SerializeField] float _changeDelay;

        AnimationSpriteCatalogue _catalogue = null;
        IAnimationCallback _animationCallback = null;
        int _spriteIndex = 0;
        private void Start()
        {
            OnStart();
        }

        public void StartAnimationWithCatalogueAndCallback(AnimationSpriteCatalogue catalogue,
            IAnimationCallback callback)
        {
            _catalogue = catalogue;
            _animationCallback = callback;
            StartAnimation();
        }

        void StartAnimation()
        {
            if (_catalogue == null) return;
            OnStartAnimation();
            DoAnimation();
        }

        private void DoAnimation()
        {
            Sequence sequence = DOTween.Sequence();

            sequence.AppendInterval(_changeDelay)
                .AppendCallback(OnAppendCallback)
                .SetLoops(_catalogue.Sprites.Length - 1)
                .OnComplete(OnCompleteCallback);
        }

        void OnStart()
        {
            _renderer.enabled = _isEnableInStart;
        }

        void OnStartAnimation()
        {
            _renderer.enabled = true;
            _spriteIndex = 0;
            SetSpriteByIndex();
        }

        void OnAppendCallback()
        {
            NextIndex();
            SetSpriteByIndex();
        }

        void OnCompleteCallback()
        {
            _renderer.enabled = _isEnableInEnd;
            _animationCallback?.OnAnimationCallback();
        }

        void NextIndex()
        {            
            if (_spriteIndex + 1 == _catalogue.Sprites.Length) return;
            _spriteIndex++;
        }

        void SetSpriteByIndex()
        {
            _renderer.sprite = _catalogue.Sprites[_spriteIndex];
        }
    }
}
