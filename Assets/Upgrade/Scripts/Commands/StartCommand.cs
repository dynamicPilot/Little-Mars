using Zenject;

namespace LittleMars.Commands
{
    public class StartCommand : ICommand
    {
        readonly LevelReceiver _receiver;

        public StartCommand(LevelReceiver receiver)
        {
            _receiver = receiver;
        }

        public void Execute()
        {
            _receiver.Start();
        }

        public class Factory : PlaceholderFactory<StartCommand>
        { }
    }
}
