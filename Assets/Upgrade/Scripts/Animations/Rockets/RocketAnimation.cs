using LittleMars.Buildings.View;
using System.Collections;
using UnityEngine;

namespace LittleMars.Animations.Rockets
{
    public class RocketAnimation : BuildingViewEffectAnimation
    {
        [SerializeField] SpriteChangeAnimation _rocket;
        [SerializeField] SpriteChangeAnimation _balks;

        [Header("Settings")]
        [SerializeField] float _balksPrepandDelay = 3f;
        [SerializeField] AnimationSpriteCatalogue _rocketArrivingSprites;
        [SerializeField] AnimationSpriteCatalogue _rocketDepartureSprites;
        [SerializeField] AnimationSpriteCatalogue _balksOnSprites;
        [SerializeField] AnimationSpriteCatalogue _balksOffSprites;

        public override void OnStartEffectAnimation(IAnimationCallback callback)
        {
            _rocket.StartAnimationWithCatalogueAndCallback(_rocketArrivingSprites, callback);
            StartCoroutine(BalksOnDelay());
        }

        public override void OnEndEffectAnimation(IAnimationCallback callback)
        {
            StopAllCoroutines();
            _rocket.StartAnimationWithCatalogueAndCallback(_rocketDepartureSprites, callback);
            _balks.StartAnimationWithCatalogueAndCallback (_balksOffSprites, null);
        }

        IEnumerator BalksOnDelay()
        {
            yield return new WaitForSeconds(_balksPrepandDelay);
            _balks.StartAnimationWithCatalogueAndCallback(_balksOnSprites, null);
        }
    }
}
