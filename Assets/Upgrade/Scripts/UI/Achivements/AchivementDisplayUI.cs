using LittleMars.Common.Signals;
using LittleMars.UI.GoalDisplays;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.UI.Achivements
{
    public class AchivementDisplayUI : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private GoalDisplayUI _displayUI;
        [SerializeField] private Button _quitButton;

        SignalBus _signalBus;
        AchivementDisplayController _controller;

        bool _isOpen = false;

        [Inject]
        public void Constructor(SignalBus signalBus, AchivementDisplayController controller)
        {
            _signalBus = signalBus;
            _controller = controller;
            _isOpen = false;

            Init();
        }

        private void Init()
        {
            _signalBus.Subscribe<AchivementReachedSignal>(OnAchivementReached);
            SetListeners();            
        }
        
        private void OnDestroy()
        {
            _signalBus.TryUnsubscribe<AchivementReachedSignal>(OnAchivementReached);
            _quitButton.onClick.RemoveAllListeners();
        }

        private void SetListeners()
        {
            _quitButton.onClick.AddListener(Close);
        }

        private void OnAchivementReached(AchivementReachedSignal args)
        {
            if (_isOpen) return;

            if (CheckStrategy(args.GoalIndex)) Open();
            else Close();
        }

        private bool CheckStrategy(int index)
        {
            Debug.Log("Check Strategy");
            var strategy = _controller.GetDisplayStrategy(index);

            if (strategy == null) return false;
            else
            {
                Debug.Log("Updating strategy");
                UpdateGoalDisplay(strategy);
                return true;
            }           
        }

        private void Open()
        {
            Debug.Log("OPEN");
            _controller.Open();
            _panel.SetActive(true);
            _isOpen = true;
        }

        private void Close()
        {
            Debug.Log("CLOSE");
            _isOpen = false;
            _panel.SetActive(false);
            _controller.Close();
        }

        private void UpdateGoalDisplay(IGoalDisplayStrategy strategy)
        {
            _displayUI.SetSlot(strategy);
        }
    }
}
