using LittleMars.AudioSystems;
using LittleMars.Commands.Level;
using LittleMars.Common;
using LittleMars.Model.TimeUpdate;
using Zenject;

namespace LittleMars.LevelMenus
{
    /// <summary>
    /// Support class for LevelMenuUI, provides time management and command management.
    /// </summary>
    public class LevelMenu : GameMenu
    {
        readonly TimeSpeedManager _timeManager;
        public LevelMenu(LevelCommandManager commandManager, TimeSpeedManager timeManager, SignalBus signalBus)
            : base(commandManager, signalBus)
        {
            _timeManager = timeManager;
        }

        public override void Open()
        {
            _timeManager.Stop();
        }
        public override void Close()
        {
            _timeManager.Start();
        }

    }
}
