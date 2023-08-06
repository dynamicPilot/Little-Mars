using DG.Tweening;
using UnityEngine;

namespace LittleMars.Animations
{
    public class TweenAnimation : MonoBehaviour
    {
        protected virtual void OnDisable()
        {
            
            DOTween.Kill(this);
        }
    }
}
