using Zenject;

namespace LittleMars.Commands
{

    public class NextCommand : ICommand
    {
        readonly LevelReceiver _receiver;

        public NextCommand(LevelReceiver receiver)
        {
            _receiver = receiver;
        }

        public void Execute()
        {
            _receiver.Next();
        }

        public class Factory : PlaceholderFactory<NextCommand>
        { }
    }
}
