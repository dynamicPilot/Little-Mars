using DG.Tweening;
using UnityEngine;

namespace LittleMars.Animations
{
    public class DOTweenAnimation : MonoBehaviour
    {

        private void OnDisable()
        {
            DOTween.Kill(this);
        }
    }
}
