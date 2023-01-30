using System;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.Commands
{
    public class CommandInvoker : IDisposable, IInitializable
    {
        Button _button;
        ICommand _command;

        public CommandInvoker(Button button, ICommand command)
        {
            _button = button;
            _command = command;
        }

        public void Initialize()
        {
            SetListener();
        }

        public void Dispose()
        {
            RemoveListener();
        }

        void SetListener()
        {
            _button.onClick.AddListener(_command.Execute);
        }

        void RemoveListener()
        {
            _button.onClick.RemoveListener(_command.Execute);
        }

        public class Factory : PlaceholderFactory<Button, ICommand, CommandInvoker>
        { }
    }
}
