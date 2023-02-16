using LittleMars.Common.Signals;
using System;
using UnityEngine;
using Zenject;

namespace LittleMars.Controllers
{
    public class InputControl : IInitializable, IDisposable
    {
        readonly SignalBus _signalBus;
        readonly Settings _settings;

        TouchControls _controls;

        public InputControl(SignalBus signalBus,Settings settings)
        {
            _signalBus = signalBus;
            _settings = settings;
            _controls = new TouchControls();
        }

        public void Initialize()
        {
            SetListeners();
            _signalBus.Subscribe<StartGameSignal>(StartControl);
            _signalBus.Subscribe<EndGameSignal>(EndControl);
        }

        public void Dispose()
        {
            _signalBus?.TryUnsubscribe<StartGameSignal>(StartControl);
            _signalBus?.TryUnsubscribe<EndGameSignal>(EndControl);
            _controls.Dispose();
        }

        void StartControl()
        {
            Debug.Log("----- >> Start Control");
            _controls.Enable();
        }

        void EndControl()
        {
            Debug.Log("<< ----- End Control");
            _controls.Disable();
        }

        void SetListeners()
        {
            _controls.ViewActionMap.PrimaryTouchContact.started += ctx => StartTouch();
            _controls.ViewActionMap.PrimaryTouchContact.canceled += ctx => StopTouch();
        }

        public Vector2 GetPrimaryTouchPosition() 
            => _controls.ViewActionMap.PrimaryTouchPosition.ReadValue<Vector2>();

        //public Vector2 GetSecondaryTouchPosition()
        //    => _controls.ViewActionMap.SecondaryTouchPosition.ReadValue<Vector2>();

        void StartTouch()
        {
            if (CheckTouchPositionForBeInMapArea())
                _signalBus.Fire<StartTouchSignal>();
        }
        
        void StopTouch() => _signalBus.Fire<EndTouchSignal>();

        bool CheckTouchPositionForBeInMapArea()
        {
            var position = GetPrimaryTouchPosition();
            return (position.y > _settings.MapYThreshold);
        }

        [Serializable]
        public class Settings
        {
            public float MapYThreshold;
        }
    }
}
