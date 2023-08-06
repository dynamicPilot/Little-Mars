using LittleMars.Commands;
using LittleMars.Common;
using LittleMars.Common.Signals;
using LittleMars.WindowManagers;
using UnityEngine;
using Zenject;

namespace LittleMars.LevelMenus
{
    public class GameMenu
    {
        readonly SceneCommandManager _commandManager;
        protected readonly SignalBus _signalBus;
        public GameMenu(SceneCommandManager commandManager, SignalBus signalBus)
        {
            _commandManager = commandManager;
            _signalBus = signalBus;
        }

        public ICommand GetCommand(CommandType type)
        {
            return _commandManager.GetCommand(type);
        }

        public void OpenWindowById(WindowID id, WindowID senderId, WindowState state)
        {
            _signalBus.TryFire(new OpenWindowByIdSignal { 
                Id = (int)id, 
                SenderId = (int) senderId, 
                NextSenderState = (int) state,
                Context = null });
        }

        public void SetWindowState(WindowID id, WindowState state)
        {
            _signalBus.TryFire(new WindowStateByIdSignal
            {
                Id = (int)id,
                SenderState = (int)state
            });
        }

        public virtual void Open()
        { }

        public virtual void Close()
        { }
    }
}
