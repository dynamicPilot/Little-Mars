using LittleMars.Commands.Level;
using Zenject;

namespace LittleMars.Commands.MainMenu
{
    public class QuitCommand : ICommand
    {
        readonly MenuReceiver _receiver;

        public QuitCommand(MenuReceiver receiver)
        {
            _receiver = receiver;
        }

        public void Execute() => _receiver.Quit();

        public class Factory : PlaceholderFactory<QuitCommand>
        { }
    }
}
