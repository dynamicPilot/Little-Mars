using LittleMars.Common;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.UI.LevelMenus
{
    public class LevelMenuUI : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;

        [Header("UI Elements")]
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _levelNumberText;
        // Level text -> need language control

        [Header("Button")]
        [SerializeField] private CommandType[] _order;
        [SerializeField] private Button _toMenuButton;

        LevelMenu _levelMenu;
        protected SignalBus _signalBus;

        protected Dictionary<CommandType, Button> _buttons;

        protected virtual void Awake()
        {
            _buttons = new Dictionary<CommandType, Button>();
            _buttons.Add(CommandType.mainMenu, _toMenuButton);
        }

        [Inject]
        public void Constructor(LevelMenu levelMenu, SignalBus signalBus)
        {
            _levelMenu = levelMenu;
            _signalBus = signalBus;

            Init();
        }

        protected virtual void Init()
        {
            
        }

        public void SetMenu(Common.Levels.LevelInfo info, MenuMode mode)
        {
            _levelNumberText.text = info.Number.ToString();
            _image.sprite = (mode == MenuMode.start) ? info.StartSprite : info.EndSprite;

            SetButtons();
        }

        public void SetButtons()
        {
            SetListeners();
            SetButtonsOrder();
        }

        protected virtual void SetListeners()
        {
            AddCommandToButtonListener(_toMenuButton, CommandType.mainMenu);
        }

        void SetButtonsOrder()
        {
            if (_buttons.Values.Count < 2) return;

            for (int i = 0; i < _order.Length; i++)
            {
                var type = _order[i];
                if (!_buttons.ContainsKey(type)) continue;

                _buttons[type].transform.SetSiblingIndex(i);
            }
        }

        protected void AddCommandToButtonListener(Button button, CommandType type)
        {
            Debug.Log("Add listeners to button " + type);
            var command = _levelMenu.GetCommand(type);
            button.onClick.AddListener(command.Execute);
            button.onClick.AddListener(Close);
        }

        void RemoveListeners()
        {
            foreach (var item in _buttons.Values)
                item.onClick.RemoveAllListeners();
        }

        protected void Open()
        {
            Debug.Log("Open menu!");
            _panel.SetActive(true);
            _levelMenu.Open();
        }

        protected void Close()
        {
            Debug.Log("Close menu!");
            _panel.SetActive(false);
            RemoveListeners();
            _levelMenu.Close();
        }
    }
}
