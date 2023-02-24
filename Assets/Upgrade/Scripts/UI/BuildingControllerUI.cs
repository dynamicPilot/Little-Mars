using LittleMars.Buildings;
using LittleMars.Common;
using LittleMars.Common.Interfaces;
using LittleMars.Common.Signals;
using LittleMars.UI.Buttons;
using LittleMars.UI.MenuControls;
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

        //IBuilding _building = null;
        IBuildingFacade _building = null;
        BuildingController _controller;
        SignalBus _signalBus;

        bool _isListenersSet = false;

        [Inject]
        public void Constructor(BuildingController controller, SignalBus signalBus)
        {
            _controller = controller;
            _signalBus = signalBus;

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

            _removeButton.onClick.AddListener(delegate { _controller.Remove(_building); });
            _removeButton.onClick.AddListener(Close);
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
            base.Open();

            _signalBus.Subscribe<BuildingStateChangedSignal>(OnBuildingStateChanged);
        }

        protected override void Close()
        {
            base.Close();
            _building = null;
            _signalBus.TryUnsubscribe<BuildingStateChangedSignal>(OnBuildingStateChanged);
        }

        private void UpdateButtonsState()
        {
            _stateButton.SetState(_building.State());
            _dayStateButton.SetState(_building.StateForPeriod(Period.day));
            _nightStateButton.SetState(_building.StateForPeriod(Period.night));
        }

        private void TryChangeState()
        {
            _controller.TryChangeState(_building);
            _stateButton.ChangeStateToOpposite();
        }

        private void ChangeTimetable(Period period)
        {
            _controller.ChangeTimetable(_building, Period.day);
            if (period == Period.day) _dayStateButton.ChangeStateToOpposite();
            else _nightStateButton.ChangeStateToOpposite();
        }

        private void OnStateChangedUpdate()
        {
            _stateButton.SetState(_building.State());
        }

        private void OnBuildingStateChanged(BuildingStateChangedSignal arg)
        {
            if (arg.BuildingFacade != _building)
                return;
            OnStateChangedUpdate();
        }


        public void CloseMenu()
        {
            Close();
        }

    }
}
