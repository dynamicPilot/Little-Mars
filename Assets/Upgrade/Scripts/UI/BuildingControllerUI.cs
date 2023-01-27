using LittleMars.Buildings;
using LittleMars.Common;
using LittleMars.Common.Interfaces;
using LittleMars.Common.Signals;
using LittleMars.UI.Buttons;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.UI
{
    /// <summary>
    /// UI for BuildingController.
    /// </summary>
    public class BuildingControllerUI : MonoBehaviour
    {
        [SerializeField] private ButtonWithStateView _stateButton;
        [SerializeField] private Button _removeButton;
        [SerializeField] private ButtonWithStateView _dayStateButton;
        [SerializeField] private ButtonWithStateView _nightStateButton;
        [SerializeField] private GameObject _panel;

        //IBuilding _building = null;
        IBuildingFacade _building = null;
        BuildingController _controller;
        SignalBus _signalBus;

        bool _isOpen = false;
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
            _signalBus.Subscribe<BuildingControllerSignal>(OnControllerStarted);
        }

        private void OnDestroy()
        {
            RemoveListeners();
            _signalBus.TryUnsubscribe<BuildingControllerSignal>(OnControllerStarted);
        }
        private void SetListeners()
        {
            _isListenersSet = true;
            _stateButton.Button.onClick.AddListener(TryChangeState);
            _removeButton.onClick.AddListener(delegate { _controller.Remove(_building); });
            _dayStateButton.Button.onClick.AddListener(delegate { ChangeTimetable(Period.day); });
            _nightStateButton.Button.onClick.AddListener(delegate { ChangeTimetable(Period.night); });
        }

        private void RemoveListeners()
        {
            _stateButton.Button.onClick.RemoveAllListeners();
            _removeButton.onClick.RemoveAllListeners();
            _dayStateButton.Button.onClick.RemoveAllListeners();
            _nightStateButton.Button.onClick.RemoveAllListeners();
        }

        public void OnControllerStarted(BuildingControllerSignal arg)
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

        private void Open()
        {
            if (_controller == null) return;
            if (!_isListenersSet) SetListeners();

            UpdateButtonsState();
            _panel.SetActive(true);
            _isOpen = true;

            _signalBus.Subscribe<BuildingStateChangedSignal>(OnBuildingStateChanged);
        }

        private void Close()
        {
            _isOpen = false;
            _panel.SetActive(false);
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
    }
}
