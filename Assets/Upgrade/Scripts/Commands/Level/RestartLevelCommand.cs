using Zenject;

namespace LittleMars.Commands.Level
{
    public class RestartLevelCommand : ICommand
    {
        readonly LevelReceiver _receiver;

        public RestartLevelCommand(LevelReceiver receiver)
        {
            _receiver = receiver;
        }

        public void Execute() => _receiver.Restart();

        public class Factory : PlaceholderFactory<RestartLevelCommand>
        { }
    }
}
