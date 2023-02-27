using LittleMars.AudioSystems;
using LittleMars.Common.Signals;
using LittleMars.StartMenus;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.UI.StartMenus
{    
    public class LangMenuUI : MenuUI
    {
        [SerializeField] Button _startButton;
        [SerializeField] CarrouselPanelUI _carrouselUI;

        SignalBus _signalBus;
        UISoundSystem _audioSystem;
        LangMenu _menu;

        [Inject]
        public void Constructor(SignalBus signalBus, LangMenu menu, UISoundSystem audioSystem)
        {
            _signalBus = signalBus;
            _menu = menu;
            _audioSystem = audioSystem;
            Init();
        }

        void Init()
        {
            _signalBus.Subscribe<NoConfigIsLoadedSignal>(OnNoConfigIsLoaded);
        }

        private void OnEnable()
        {
            _startButton.onClick.AddListener(Close);
        }

        private void OnDestroy()
        {
            _startButton.onClick.RemoveAllListeners();
            _signalBus?.TryUnsubscribe<NoConfigIsLoadedSignal>(OnNoConfigIsLoaded);
        }

        void OnNoConfigIsLoaded(NoConfigIsLoadedSignal args)
        {
            if (_isOpen) return;
            
            StartLangMenu(args.PlayerConfig);
        }

        void StartLangMenu(PlayerConfig config)
        {
            _menu.Open(config);
            _carrouselUI.SetIndex(config.Lang);
            Open();
        }

        protected override void Close()
        {
            base.Close();

            _audioSystem.PlayUISound(Common.UISoundType.clickThird);
            var index = _carrouselUI.GetIndex();
            _menu.SetLangIndex(index);
            _menu.Close();
        }
    }
}
