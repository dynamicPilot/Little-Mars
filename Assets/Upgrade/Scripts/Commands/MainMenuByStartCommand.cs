using Zenject;

namespace LittleMars.Commands
{
    public class MainMenuByStartCommand : ICommand
    {
        readonly LevelReceiver _receiver;

        public MainMenuByStartCommand(LevelReceiver receiver)
        {
            _receiver = receiver;
        }

        public void Execute() => _receiver.MainMenuByStart();

        public class Factory : PlaceholderFactory<MainMenuByStartCommand>
        { }
    }
}
