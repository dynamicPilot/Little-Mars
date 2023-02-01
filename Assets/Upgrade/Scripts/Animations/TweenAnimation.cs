using DG.Tweening;
using UnityEngine;

namespace LittleMars.Animations
{
    public class TweenAnimation : MonoBehaviour
    {

        private void OnDisable()
        {
            DOTween.Kill(this);
        }
    }
}
