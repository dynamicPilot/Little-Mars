using LittleMars.Commands.Level;
using Zenject;

namespace LittleMars.Commands
{

    public class MainMenuCommand : IExtendedCommand
    {
        Receiver _receiver;

        //public MainMenuCommand(Receiver receiver = null)
        //{
        //    _receiver = receiver;
        //}

        public void Execute() => _receiver?.MainMenu();
        public void ChangeReceiver(Receiver receiver) => _receiver = receiver;
        public class Factory : PlaceholderFactory<MainMenuCommand>
        { }
    }
}
