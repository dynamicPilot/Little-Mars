using Zenject;

namespace LittleMars.Commands.Level
{
    public class GoalInfoCommand : ICommand
    {
        readonly LevelReceiver _receiver;

        public GoalInfoCommand(LevelReceiver receiver)
        {
            _receiver = receiver;
        }

        public void Execute() => _receiver.GoalInfo();

        public class Factory : PlaceholderFactory<GoalInfoCommand>
        { }
    }
}
