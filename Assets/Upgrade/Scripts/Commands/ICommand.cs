using LittleMars.Common;

namespace LittleMars.Commands
{
    public interface ICommand
    {
        void Execute();
    }

    public interface IExtendedCommand : ICommand
    {
        void ChangeReceiver(Receiver receiver);
    }

    public interface ICommandManager
    {
        ICommand GetCommand(CommandType type);
    }
}
