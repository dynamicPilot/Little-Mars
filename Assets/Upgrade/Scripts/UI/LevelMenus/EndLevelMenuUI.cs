using LittleMars.AudioSystems;
using LittleMars.Common;
using LittleMars.Common.Signals;
using LittleMars.LevelMenus;
using LittleMars.WindowManagers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.UI.LevelMenus
{
    public class EndLevelMenuUI : LevelMenuUIWithControls
    {
        //[Header("Buttons")]
        //[SerializeField] private Button _nextButton;
        //[SerializeField] private Button _restartButton;

        //protected override void Awake()
        //{
        //    base.Awake();
        //    _buttons.Add(CommandType.next, _nextButton);
        //    _buttons.Add(CommandType.restart, _restartButton);
        //}

        [Inject]
        public void Constructor(LevelMenu levelMenu, Common.Levels.LevelInfo levelInfo,
            SoundsForGameMenuUI sounds)
        {
            base.BaseConstructor(levelMenu, levelInfo, sounds);
        }

        protected override void Init() => SetButtons();
        //{
        //_signalBus.Subscribe<EndGameSignal>(OnEndGame);
        //}

        //protected override void SetListeners()
        //{
        //    base.SetListeners();
        //    AddCommandToButtonListener(_nextButton, CommandType.next);
        //    AddCommandToButtonListener(_restartButton, CommandType.restart);
        //}

        public override void OnOpenMenu(WindowContext context)
        {
            SetMenu();
            //SetButtons();
            base.OnOpenMenu(context);
        }
    }
}
