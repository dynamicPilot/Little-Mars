﻿using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace LittleMars.Animations
{
    public class SliderFillingAnimation : TweenAnimationUI
    {
        [SerializeField] private Slider _slider;

        [Header("Animation Settings")]
        [SerializeField] private float _duration = 1f;
        [SerializeField] private float _startValue = 0;
        [SerializeField] private float _endValue = 1f;
        [SerializeField] private bool _unscaledTime = false;

        public override void StartAnimation()
        {
            CheckValues();            
            FillingIndicator();
        }

        void FillingIndicator()
        {
            //Debug.Log("Filling....." + _unscaledTime);
            _slider.value = _startValue;
            _slider.DOValue(_endValue, _duration).SetUpdate(_unscaledTime).SetId(_id);
        }

        void CheckValues()
        {
            if (_endValue > _slider.maxValue) _endValue = _slider.maxValue;
            if (_startValue < _slider.minValue) _startValue = _slider.minValue;
        }
    }
}
