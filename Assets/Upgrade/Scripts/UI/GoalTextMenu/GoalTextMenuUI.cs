using LittleMars.AudioSystems;
using LittleMars.UI.LevelMenus;
using TMPro;
using UnityEngine;
using Zenject;

namespace LittleMars.UI.GoalTextMenu
{
    public class GoalTextMenuUI : MenuUIWithControls
    {
        [Header("Text Slot")]
        [SerializeField] TextMeshProUGUI[] _texts;

        GoalTextLevelMenu _goalTextMenu;
        //SignalBus _signalBus;
        bool _isTextSet = false;
        //protected override void Awake()
        //{
        //    base.Awake();
        //}

        [Inject]
        public void Constructor(GoalTextLevelMenu levelMenu, SoundsForGameMenuUI sounds)
        {
            _gameMenu = levelMenu;
            _goalTextMenu = levelMenu;
            _sounds = sounds;

            Init();
        }

        public void Init()
        {
            //_signalBus.Subscribe<NeedGoalInfoSignal>(OnOpenButtonClicked);
            SetButtons();
        }

        private void OnDestroy()
        {
            RemoveListeners();
            //_signalBus?.TryUnsubscribe<NeedGoalInfoSignal>(OnOpenButtonClicked);
        }


        //protected override void Close()
        //{
        //    base.Close();
        //    _sounds.PlaySoundForCommandType(CommandType.quit);
        //}

        public override void OnOpenMenu()
        {
            //if (_isOpen) return;
            //_sounds.PlaySoundForCommandType(CommandType.empty);
            if (!_isTextSet) SetGoalTexts();
        }

        private void SetGoalTexts()
        {
            var texts = _goalTextMenu.GetGoalTexts();
            if (texts == null) return;

            _isTextSet = true;
            int index = 0;

            for (int i = 0; i < texts.Length; i++)
            {
                if (i >= _texts.Length)
                {
                    return;
                }

                _texts[i].gameObject.SetActive(true);
                _texts[i].text = texts[i];
                index++;
            }

            if (index < _texts.Length)
            {
                for (int i = index; i < _texts.Length; i++)
                    _texts[i].gameObject.SetActive(false);
            }

        }
    }
}
