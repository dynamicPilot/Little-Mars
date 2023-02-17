using LittleMars.Common;
using LittleMars.UI.LevelMenus;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.UI.GoalTextMenu
{
    public class GoalTextMenuUI : GameMenuUI
    {
        [Header("OpenButton")]
        [SerializeField] private Button _openButton;

        [Header("Text Slot")]
        [SerializeField] private TextMeshProUGUI[] _texts;

        GoalTextLevelMenu _goalTextMenu;
        bool _isTextSet = false;
        protected override void Awake()
        {
            base.Awake();
            _buttons.Add(CommandType.empty, _openButton);
        }

        [Inject]
        public void Constructor(GoalTextLevelMenu levelMenu)
        {
            _gameMenu = levelMenu;
            _goalTextMenu = levelMenu;

            Init();
        }

        public void Init()
        {
            SetButtons();
        }

        private void OnDestroy()
        {
            RemoveListeners();
        }

        protected override void SetListeners()
        {
            base.SetListeners();
            _openButton.onClick.AddListener(OnOpenButtonClicked);
        }

        protected override void Close()
        {
            base.Close();
            _openButton.interactable = true;
        }

        private void OnOpenButtonClicked()
        {
            _openButton.interactable = false;
            if (!_isTextSet) SetGoalTexts();
            Open();
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
