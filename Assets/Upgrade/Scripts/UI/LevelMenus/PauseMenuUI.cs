using LittleMars.AudioSystems;
using LittleMars.Common;
using LittleMars.LevelMenus;
using LittleMars.WindowManagers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.UI.LevelMenus
{
    public class PauseMenuUI : MenuUIWithControls
    {
        [SerializeField] PauseAudioSettingsUI _audioUI;
        PauseLevelMenu _pauseMenu;

        [Inject]
        public void Constructor(PauseLevelMenu levelMenu, SoundsForGameMenuUI sounds)
        {
            _pauseMenu = levelMenu;
            _gameMenu = levelMenu;
            _sounds = sounds;

            Init();
        }
        void Init() => SetButtons();
        void OnDestroy() => RemoveListeners();

        public override void OnOpenMenu(WindowContext context)
        {
            _sounds.PlaySoundForCommandType(CommandType.empty);
            _audioUI.SetParameters();
            base.OnOpenMenu(context);
        }

        public override void OnCloseMenu()
        {
            if (_audioUI.NeedSave())
                _pauseMenu.SaveSettings();
            base.OnCloseMenu();
        }
    }
}
