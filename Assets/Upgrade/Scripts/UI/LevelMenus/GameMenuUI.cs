using LittleMars.Common;
using LittleMars.LevelMenus;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LittleMars.UI.LevelMenus
{
    /// <summary>
    /// Menu UI with command manager support for buttons by GameMenu script.
    /// Has no constructor -> set _gameMenu manually.
    /// </summary>
    public class GameMenuUI : MenuUI
    {
        [Header("Common Button")]
        [SerializeField] private CommandType _buttonType = CommandType.mainMenu;
        [SerializeField] private Button _commonButton;

        protected Dictionary<CommandType, Button> _buttons;
        protected GameMenu _gameMenu;

        protected override void Awake()
        {
            _buttons = new Dictionary<CommandType, Button>();
            _buttons.Add(_buttonType, _commonButton);
        }

        public void SetButtons()
        {
            SetListeners();
        }

        protected virtual void SetListeners()
        {
            AddCommandToButtonListener(_commonButton, _buttonType);
        }

        protected virtual void RemoveListeners()
        {
            foreach (var item in _buttons.Values)
                item.onClick.RemoveAllListeners();
        }

        protected void AddCommandToButtonListener(Button button, CommandType type,
            bool needClose = true)
        {
            //Debug.Log("Add listeners to button " + type);
            var command = _gameMenu.GetCommand(type);
            if (command == null) return;

            //Debug.Log("Get command for type " + type + " command " + (command == null));
            button.onClick.AddListener(command.Execute);

            //Debug.Log("add close? " + needClose);
            if (needClose) button.onClick.AddListener(Close);
        }

        protected override void Open()
        {
            base.Open();
            _gameMenu.Open();
        }

        protected override void Close()
        {
            base.Close();
            _gameMenu.Close();
        }
    }
}
