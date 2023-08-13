using LittleMars.Common;
using UnityEngine;

namespace LittleMars.Commands.MainMenu
{
    public class MainMenuCommandManager : SceneCommandManager
    {
        readonly MenuReceiver _menuReceiver;

        public MainMenuCommandManager(MenuReceiver menuReceiver, ProjectCommandManager projectManager) 
            : base(projectManager)
        {
            _menuReceiver = menuReceiver;
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
                default:
                    _commands.Add(type, GetCommandFromProjectManager(type, _menuReceiver));
                    break;
            }
        }
    }
}
