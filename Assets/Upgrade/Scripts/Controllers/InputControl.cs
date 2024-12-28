using LittleMars.Common.Signals;
using LittleMars.Signals;
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
        bool _isViewActionMapEnabled = false;
        bool _isTooltipOnScreen = false;
        bool _isLocked = false;

        public InputControl(SignalBus signalBus,Settings settings)
        {
            _signalBus = signalBus;
            _settings = settings;
            _controls = new TouchControls();

            _isViewActionMapEnabled = false;
            _isTooltipOnScreen = false;
            _isLocked = false;
        }

        public void Initialize()
        {
            SetListeners();
            _signalBus.Subscribe<StartGameSignal>(StartControl);
            _signalBus.Subscribe<EndGameSignal>(EndControl);

            _signalBus.Subscribe<CallForTooltipSignal>(OnTooltipOnScreen);
            _signalBus.Subscribe<CallForHideTooltipSignal>(OnTooltipHide);

            _signalBus.Subscribe<BeginBuildingDragSignal>(OnBeginLocked);
            _signalBus.Subscribe<EndBuildingDragSignal>(OnEndLocked);

            _controls.Enable();
        }

        public void Dispose()
        {
            _signalBus?.TryUnsubscribe<StartGameSignal>(StartControl);
            _signalBus?.TryUnsubscribe<EndGameSignal>(EndControl);

            _signalBus?.TryUnsubscribe<CallForTooltipSignal>(OnTooltipOnScreen);
            _signalBus?.TryUnsubscribe<CallForHideTooltipSignal>(OnTooltipHide);

            _signalBus?.TryUnsubscribe<BeginBuildingDragSignal>(OnBeginLocked);
            _signalBus?.TryUnsubscribe<EndBuildingDragSignal>(OnEndLocked);

            _controls.Dispose();
            //BeginBuildingDragSignal
        }

        void StartControl()
        {
            Debug.Log("----- >> Start Control");
            _isViewActionMapEnabled = true;            
        }

        void EndControl()
        {
            Debug.Log("<< ----- End Control");
            _isViewActionMapEnabled = false;
            _isTooltipOnScreen = false;
            _isLocked = false;
            _controls.Disable();
        }

        void SetListeners()
        {
            _controls.ViewActionMap.PrimaryTouchContact.started += ctx => StartTouch();           
            _controls.ViewActionMap.PrimaryTouchContact.canceled += ctx => StopTouch();

            _controls.TooltipActionMap.PrimaryTouchContact.canceled += ctx => TooltipStartTouch();
        }

        public Vector2 GetPrimaryTouchPosition() 
            => _controls.ViewActionMap.PrimaryTouchPosition.ReadValue<Vector2>();

        //public Vector2 GetSecondaryTouchPosition()
        //    => _controls.ViewActionMap.SecondaryTouchPosition.ReadValue<Vector2>();

        void StartTouch()
        {
            if (CheckTouchPositionForBeInMapArea() && _isViewActionMapEnabled && !_isLocked)
                _signalBus.Fire<StartTouchSignal>();
        }

        void StopTouch()
        {
            if (_isViewActionMapEnabled)
                _signalBus.Fire<EndTouchSignal>();
        }

        void OnTooltipOnScreen()
        {
            _isTooltipOnScreen = true;
        }

        void OnTooltipHide()
        {
            _isTooltipOnScreen = false;
        }

        void OnBeginLocked()
        {
            _isLocked = true;
        }

        void OnEndLocked()
        {
            _isLocked = false;
        }

        void TooltipStartTouch()
        {
            if (_isTooltipOnScreen)
                _signalBus.Fire<TooltipStartTouchSignal>();
        }

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
