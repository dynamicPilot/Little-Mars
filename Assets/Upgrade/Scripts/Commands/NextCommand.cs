using Zenject;

namespace LittleMars.Commands
{

    public class NextCommand : ICommand
    {
        readonly Receiver _receiver;

        public NextCommand(Receiver receiver)
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
