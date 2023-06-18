using LittleMars.AudioSystems;
using LittleMars.LevelMenus;
using LittleMars.UI.LevelMenus;
using Zenject;

namespace LittleMars.UI.MainMenu
{
    public class MainMenuUI : MenuUIWithControls
    {
        protected override void Awake()
        {
            base.Awake();
            _isOpen = true;
        }

        private void OnDestroy()
        {
            RemoveListeners();
        }

        [Inject]
        public void Constructor(GameMenu gameMenu, SoundsForGameMenuUI sounds)
        {
            _gameMenu = gameMenu;
            _sounds = sounds;
            Init();
        }

        void Init()
        {
            SetButtons();
        }
    }
}
