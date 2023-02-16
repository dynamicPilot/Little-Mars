using LittleMars.CameraControls;
using LittleMars.Common.Signals;
using System;
using UnityEngine;
using Zenject;

namespace LittleMars.Controllers
{
    public class InputMoveDetection : ITickable, IInitializable, IDisposable
    {
        const int REVERSE_MOVE_MULTIPLIER = -1;

        readonly Settings _settings;
        readonly InputControl _inputControl;
        readonly CameraViewPositioner _positioner;
        readonly CameraMover _cameraMover;
        readonly SignalBus _signalBus;

        bool _pauseUpdate;
        bool _inDetection;
        bool _inMove;

        float _timer;
        float _duration;
        Vector2 _touchPosition;

        public InputMoveDetection(Settings settings, InputControl inputControl, 
            CameraViewPositioner positioner, SignalBus signalBus, CameraMover cameraMover)
        {
            _settings = settings;
            _inputControl = inputControl;
            _positioner = positioner;
            _signalBus = signalBus;
            _cameraMover = cameraMover;

            _pauseUpdate = true;            
        }
        public void Initialize()
        {
            _signalBus.Subscribe<StartTouchSignal>(MoveStart);
            _signalBus.Subscribe<EndTouchSignal>(MoveStop);
        }

        public void Dispose()
        {
            _signalBus?.TryUnsubscribe<StartTouchSignal>(MoveStart);
            _signalBus?.TryUnsubscribe<EndTouchSignal>(MoveStop);
        }

        public void Tick()
        {
            if (_pauseUpdate) return;

            _duration = Time.deltaTime;
            _timer += _duration;

            if (_inDetection) InDetectionMode();
            if (_inMove) InMoveMode();
        }

        void MoveStart()
        {
            if (Time.timeScale == 0) return;

            _timer = 0f;
            _inMove = false;           
            _pauseUpdate = false;
            _inDetection = true;
        }

        void MoveStop() => _pauseUpdate = true;

        void InDetectionMode()
        {
            _inMove = (_timer >= _settings.DetectionThreshold);
            _inDetection = !_inMove;

            if (_inMove) ReadyForBeInMoveMode();
        }

        void ReadyForBeInMoveMode()
        {
            _touchPosition = _inputControl.GetPrimaryTouchPosition();
            _positioner.UpdateParams();
        }

        void InMoveMode()
        {
            var newTouchPosition = _inputControl.GetPrimaryTouchPosition();
            var positionDelta = (newTouchPosition - _touchPosition);

            Move(positionDelta);
            _touchPosition = newTouchPosition;
        }

        void Move(Vector2 positionDelta)
        {
            var delta = _positioner.GetClarifiedMovingDelta(positionDelta * _settings.DistanceScale * REVERSE_MOVE_MULTIPLIER);
            _cameraMover.Move(delta, _duration);
        }

        [Serializable]
        public class Settings
        {
            public float DetectionThreshold;
            public float DistanceScale;
        }
    }
}
