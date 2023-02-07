using LittleMars.Common;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LittleMars.UI.LevelMenus
{
    public class GameMenuUI : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;

        [Header("Common Button")]
        [SerializeField] private CommandType _buttonType = CommandType.mainMenu;
        [SerializeField] private Button _commonButton;

        protected Dictionary<CommandType, Button> _buttons;
        protected LevelMenu _levelMenu;
        protected bool _isOpen;
        protected virtual void Awake()
        {
            _isOpen = false;
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

        protected void RemoveListeners()
        {
            foreach (var item in _buttons.Values)
                item.onClick.RemoveAllListeners();
        }

        //void SetButtonsOrder()
        //{
        //    if (_buttons.Values.Count < 2) return;

        //    for (int i = 0; i < _order.Length; i++)
        //    {
        //        var type = _order[i];
        //        if (!_buttons.ContainsKey(type)) continue;

        //        _buttons[type].transform.SetSiblingIndex(i);
        //    }
        //}

        protected void AddCommandToButtonListener(Button button, CommandType type,
            bool needClose = true)
        {
            Debug.Log("Add listeners to button " + type);
            var command = _levelMenu.GetCommand(type);
            if (command == null) return;

            Debug.Log("Get command for type " + type + " command " + (command == null));
            button.onClick.AddListener(command.Execute);

            Debug.Log("add close? " + needClose);
            if (needClose) button.onClick.AddListener(Close);
        }

        protected virtual void Open()
        {
            Debug.Log("Open menu!");
            _panel.SetActive(true);
            _levelMenu.Open();
            _isOpen = true;
        }

        protected virtual void Close()
        {
            Debug.Log("Close menu!");
            _panel.SetActive(false);
            _levelMenu.Close();
            _isOpen = false;
        }
    }
}
