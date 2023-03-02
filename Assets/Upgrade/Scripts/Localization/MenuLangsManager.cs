using LittleMars.Common;
using LittleMars.Configs;

namespace LittleMars.Localization
{
    public class MenuLangsManager : LangManagerBase, ILevelLangManager
    {
        public MenuLangsManager(LangSettings playerSettings) 
            : base(playerSettings, TagGroup.level)
        {
        }
    }
}
