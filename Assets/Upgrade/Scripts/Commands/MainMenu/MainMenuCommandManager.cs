using LittleMars.Commands.Level;
using LittleMars.Common;
using UnityEngine;

namespace LittleMars.Commands.MainMenu
{
    public class MainMenuCommandManager : SceneCommandManager
    {
        readonly MenuReceiver _menuReceiver;
        readonly QuitCommand.Factory _quitFactory;

        public MainMenuCommandManager(MenuReceiver menuReceiver, ProjectCommandManager projectManager,
            QuitCommand.Factory quitFactory) 
            : base(projectManager)
        {
            _menuReceiver = menuReceiver;
            _quitFactory = quitFactory;
        }
        protected override void CreateCommand(CommandType type)
        {
            //Debug.Log($"Need command for {type}");
            switch (type)
            {
                case CommandType.next:
                    _commands.Add(type, GetCommandFromProjectManager(type, _menuReceiver));
                    break;
                case CommandType.mainMenu:
                    _commands.Add(type, GetCommandFromProjectManager(type, _menuReceiver));
                    break;
                case CommandType.quit:
                    _commands.Add(type, _quitFactory.Create());
                    break;
                default:
                    _commands.Add(type, GetCommandFromProjectManager(type, _menuReceiver));
                    break;
            }
        }
    }
}
