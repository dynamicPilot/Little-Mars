using LittleMars.AudioSystems;
using LittleMars.Buildings;
using LittleMars.Common;
using LittleMars.Common.Interfaces;
using LittleMars.Common.Signals;
using LittleMars.UI.Buttons;
using LittleMars.UI.MenuControls;
using LittleMars.UI.SlotUIFactories;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.UI
{
    /// <summary>
    /// UI for BuildingController.
    /// </summary>
    public class BuildingControllerUI : MenuUI, ICloseMenuUI
    {
        [SerializeField] ButtonWithStateView _stateButton;
        [SerializeField] Button _removeButton;
        [SerializeField] ButtonWithStateView _dayStateButton;
        [SerializeField] ButtonWithStateView _nightStateButton;
        [SerializeField] SlotUI _targetSlot;

        IBuildingFacade _building = null;
        BuildingController _controller;
        SignalBus _signalBus;
        UISoundSystem _audioSystem;
        BuildingSlotUISetter _slotSetter;

        bool _isListenersSet = false;

        [Inject]
        public void Constructor(BuildingController controller, SignalBus signalBus, 
            UISoundSystem audioSystem, BuildingSlotUISetter slotSetter)
        {
            _controller = controller;
            _signalBus = signalBus;
            _audioSystem = audioSystem;
            _slotSetter = slotSetter;

            Init();
        }

        private void Init()
        {          
            _signalBus.Subscribe<BuildingControllerOpenSignal>(OnControllerStarted);
        }

        private void OnDestroy()
        {
            RemoveListeners();
            _signalBus.TryUnsubscribe<BuildingControllerOpenSignal>(OnControllerStarted);
        }
        private void SetListeners()
        {
            _isListenersSet = true;
            _stateButton.Button.onClick.AddListener(TryChangeState);
            
            _dayStateButton.Button.onClick.AddListener(delegate { ChangeTimetable(Period.day); });
            _nightStateButton.Button.onClick.AddListener(delegate { ChangeTimetable(Period.night); });

            _removeButton.onClick.AddListener(Remove);
        }

        private void RemoveListeners()
        {
            _stateButton.Button.onClick.RemoveAllListeners();
            _removeButton.onClick.RemoveAllListeners();
            _dayStateButton.Button.onClick.RemoveAllListeners();
            _nightStateButton.Button.onClick.RemoveAllListeners();
        }

        public void OnControllerStarted(BuildingControllerOpenSignal arg)
        {
            if (arg.BuildingFacade == _building && _isOpen)
            {
                Close();
                return;
            }

            if (_isOpen) Close();
            _building = arg.BuildingFacade;
            Open();
        }

        protected override void Open()
        {
            if (_controller == null) return;
            if (!_isListenersSet) SetListeners();

            UpdateButtonsState();
            UpdateTargetImageState();
            base.Open();

            _signalBus.Subscribe<BuildingStateChangedSignal>(OnBuildingStateChanged);
        }

        public void CloseMenu()
        {
            Close();
        }

        protected override void Close()
        {
            base.Close();
            _building = null;
            _signalBus.TryUnsubscribe<BuildingStateChangedSignal>(OnBuildingStateChanged);
        }

        void UpdateButtonsState()
        {
            _stateButton.SetState(_building.State());
            Debug.Log("State for period. Day: " + _building.StateForPeriod(Period.day));
            _dayStateButton.SetState(_building.StateForPeriod(Period.day));
            Debug.Log("State for period. Night: " + _building.StateForPeriod(Period.night));
            _nightStateButton.SetState(_building.StateForPeriod(Period.night));
        }

        void UpdateTargetImageState()
        {
            BuildingType type = _building.Info().Type;
            _slotSetter.SetSlot(_targetSlot, (int) type);
        }

        void TryChangeState()
        {
            _controller.TryChangeState(_building);
            var state = _stateButton.ChangeStateToOpposite();
            PlayTurnOnOffSound(state);
        }

        void PlayTurnOnOffSound(States state)
        {
            if (state == States.on) _audioSystem.PlayUISound(UISoundType.turnOn);
            else _audioSystem.PlayUISound(UISoundType.turnOff);
        }

        void ChangeTimetable(Period period)
        {
            _controller.ChangeTimetable(_building, period);

            if (period == Period.day) _dayStateButton.ChangeStateToOpposite();
            else _nightStateButton.ChangeStateToOpposite();

            _audioSystem.PlayUISound(UISoundType.clickFirst);
        }

        void Remove()
        {
            _audioSystem.PlayUISound(UISoundType.destroy);
            _controller.Remove(_building);
            Close();
        }

        void OnStateChangedUpdate()
        {
            Debug.Log("BuildingControllerUI: button change request");
            _stateButton.SetState(_building.State());
        }

        void OnBuildingStateChanged(BuildingStateChangedSignal arg)
        {
            if (arg.BuildingFacade != _building)
                return;
            OnStateChangedUpdate();
        }
    }
}
