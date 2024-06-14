using LittleMars.AudioSystems;
using LittleMars.Common;
using LittleMars.Common.Signals;
using LittleMars.WindowManagers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.UI
{
    public class SideMenuUI : MonoBehaviour
    {
        [SerializeField] Button _openButton;
        [SerializeField] Button _infoButton;
        [SerializeField] Button _menuButton;

        SignalBus _signalBus;
        UISoundSystem _audioSystem;
        private void Start()
        {
            _openButton.onClick.AddListener(OnStateButtonClick);
            _menuButton.onClick.AddListener(OnMenuButtonClick);
            _infoButton.onClick.AddListener(OnInfoButtonClick);
        }

        private void OnDestroy()
        {
            _openButton.onClick.RemoveListener(OnStateButtonClick);
            _menuButton.onClick.RemoveListener(OnMenuButtonClick);
            _infoButton.onClick.RemoveListener(OnInfoButtonClick);
        }

        [Inject]
        public void Constructor(SignalBus signalBus, UISoundSystem audioSystem)
        {
            _signalBus = signalBus;
            _audioSystem = audioSystem;
        }

        void OnStateButtonClick()
        {
            _audioSystem.PlayUISound(UISoundType.clickThird);

            //Debug.Log("Button click");
            // for test -> better to level workflow
            _signalBus.TryFire(new OpenWindowByIdSignal
            {
                Id = (int)WindowID.level_state,
                SenderId = -1,
                NextSenderState = 0,
                Context = null
            });
        }

        void OnInfoButtonClick()
        {
            _audioSystem.PlayUISound(UISoundType.clickThird);

            //Debug.Log("Button click");
            // for test -> better to level workflow
            _signalBus.TryFire(new OpenWindowByIdSignal
            {
                Id = (int)WindowID.level_goalsInfo,
                SenderId = -1,
                NextSenderState = 0,
                Context = null
            });
        }

        void OnMenuButtonClick()
        {
            _audioSystem.PlayUISound(UISoundType.clickThird);

            //Debug.Log("Button click");
            // for test -> better to level workflow
            _signalBus.TryFire(new OpenWindowByIdSignal
            {
                Id = (int)WindowID.level_pause,
                SenderId = -1,
                NextSenderState = 0,
                Context = null
            });
        }
    }
}
