using LittleMars.Commands.Level;
using LittleMars.Common.Signals;
using LittleMars.Model.TimeUpdate;
using Zenject;

namespace LittleMars.LevelMenus
{
    public class PauseLevelMenu : LevelMenu
    {
        readonly SignalBus _signalBus;
        public PauseLevelMenu(LevelCommandManager commandManager, TimeSpeedManager timeManager, SignalBus signalBus) 
            : base(commandManager, timeManager)
        {
            _signalBus = signalBus;
        }

        public void SaveSettings()
        {
            _signalBus.Fire<NeedSaveConfigSignal>();
        }
    }
}
