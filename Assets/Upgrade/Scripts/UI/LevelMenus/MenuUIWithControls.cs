using LittleMars.AudioSystems;
using LittleMars.Common;
using LittleMars.LevelMenus;
using LittleMars.WindowManagers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LittleMars.UI.LevelMenus
{
    /// <summary>
    /// MenuUI for windowObject.
    /// Must set GameMenu and SoundsForGameMenuUI.
    /// </summary>
    public class MenuUIWithControls : MenuUI
    {
        [Header("Window ID")]
        [SerializeField] protected WindowID _id;

        [Header("Command Buttons")]
        [SerializeField] List<CommandSenderUnit<Button>> _commandButtons;

        [Header("Windows Control Buttons")]
        [SerializeField] List<WindowControlWithStateUnit> _windowButtons;

        protected GameMenu _gameMenu;
        protected SoundsForGameMenuUI _sounds;

        public void SetButtons()
        {
            SetListeners();
        }

        protected virtual void SetListeners()
        {
            foreach (var unit in _commandButtons)
                AddCommandToButtonListener(unit.Unit, unit.CommandType);

            foreach (var unit in _windowButtons)
                AddWindowsIDToButtonListener(unit.Unit, unit.WindowID, unit.SenderState);
        }

        protected virtual void RemoveListeners()
        {
            foreach (var unit in _commandButtons)
                unit.Unit.onClick.RemoveAllListeners();

            foreach (var unit in _windowButtons)
                unit.Unit.onClick.RemoveAllListeners();
        }

        protected void AddCommandToButtonListener(Button button, CommandType type)
        {
            // get command of the type
            var command = _gameMenu.GetCommand(type);
            if (command == null) return;

            // set command.Execute() as listener
            button.onClick.AddListener(command.Execute);

            if (_sounds != null)
                button.onClick.AddListener(delegate { _sounds.PlaySoundForCommandType(type); });
        }

        protected void AddWindowsIDToButtonListener(Button button, WindowID id,
            WindowState state)
        {
            button.onClick.AddListener(delegate { 
                _gameMenu.OpenWindowById(id, _id, state); });

            if (_sounds != null)
                button.onClick.AddListener(delegate { _sounds.PlaySoundForCommandType(CommandType.back); });
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
