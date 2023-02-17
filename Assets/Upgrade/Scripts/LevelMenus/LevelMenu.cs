using LittleMars.Commands.Level;
using LittleMars.Model.TimeUpdate;

namespace LittleMars.LevelMenus
{
    /// <summary>
    /// Support class for LevelMenuUI, provides time management and command management.
    /// </summary>
    public class LevelMenu : GameMenu
    {
        readonly TimeSpeedManager _timeManager;

        public LevelMenu(LevelCommandManager commandManager, TimeSpeedManager timeManager)
            : base(commandManager)
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
