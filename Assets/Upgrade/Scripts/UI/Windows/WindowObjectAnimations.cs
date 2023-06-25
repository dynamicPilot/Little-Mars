using DG.Tweening;
using LittleMars.Animations;
using System.Collections.Generic;
using UnityEngine;

namespace LittleMars.UI.Windows
{
    public class WindowObjectAnimations : MonoBehaviour
    {
        //[SerializeField] List<TweenAnimationUI> _animations;

        [Header("Settings")]
        [SerializeField] bool _killAllOnDisable = false;
        [SerializeField] bool _killAllOnDestroy = false;
        private void OnDisable()
        {
            if (_killAllOnDisable) DOTween.KillAll();
        }

        private void OnDestroy()
        {
            if (_killAllOnDestroy) DOTween.KillAll();
        }
        //public void OnOpen()
        //{
        //    ChangeAnimationsState(true);
        //}

        //public void OnClose()
        //{
        //    ChangeAnimationsState(false);
        //}

        //void ChangeAnimationsState(bool state)
        //{
        //    foreach(var animation in _animations) animation.enabled = state;
        //}


    }
}
