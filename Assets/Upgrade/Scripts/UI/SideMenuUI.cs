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
        [SerializeField] private Button _openButton;

        SignalBus _signalBus;
        UISoundSystem _audioSystem;
        private void Start()
        {
            _openButton.onClick.AddListener(OnButtonClick);
        }

        private void OnDestroy()
        {
            _openButton.onClick.RemoveListener(OnButtonClick);
        }

        [Inject]
        public void Constructor(SignalBus signalBus, UISoundSystem audioSystem)
        {
            _signalBus = signalBus;
            _audioSystem = audioSystem;
        }

        void OnButtonClick()
        {
            _audioSystem.PlayUISound(UISoundType.clickThird);

            Debug.Log("Button click");
            // for test -> better to level workflow
            _signalBus.TryFire(new OpenWindowByIdSignal
            {
                Id = (int)WindowID.level_state,
                SenderId = -1,
                NextSenderState = 0,
                Context = null
            });
        }
    }
}
