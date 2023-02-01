using LittleMars.Common;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LittleMars.UI.LevelMenus
{
    public class GameMenuUI : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;

        [Header("Button")]
        //[SerializeField] private CommandType[] _order;
        [SerializeField] private Button _toMenuButton;

        protected Dictionary<CommandType, Button> _buttons;
        protected LevelMenu _levelMenu;
        protected virtual void Awake()
        {
            _buttons = new Dictionary<CommandType, Button>();
            _buttons.Add(CommandType.mainMenu, _toMenuButton);
        }

        public void SetButtons()
        {
            SetListeners();
        }

        protected virtual void SetListeners()
        {
            AddCommandToButtonListener(_toMenuButton, CommandType.mainMenu);
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
            button.onClick.AddListener(command.Execute);
            if (needClose) button.onClick.AddListener(Close);
        }

        protected void Open()
        {
            Debug.Log("Open menu!");
            _panel.SetActive(true);
            _levelMenu.Open();
        }

        protected virtual void Close()
        {
            Debug.Log("Close menu!");
            _panel.SetActive(false);
            _levelMenu.Close();
        }
    }
}
