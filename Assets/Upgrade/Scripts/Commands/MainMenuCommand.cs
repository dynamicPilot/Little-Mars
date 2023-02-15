using Zenject;

namespace LittleMars.Commands
{

    public class MainMenuCommand : ICommand
    {
        readonly LevelReceiver _receiver;

        public MainMenuCommand(LevelReceiver receiver)
        {
            _receiver = receiver;
        }

        public void Execute() => _receiver.MainMenu();

        public class Factory : PlaceholderFactory<MainMenuCommand>
        { }
    }
}
