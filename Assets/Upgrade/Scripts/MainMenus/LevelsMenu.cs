using LittleMars.Common.Catalogues;
using LittleMars.Settings;

namespace LittleMars.MainMenus
{
    public class LevelsMenu
    {
        readonly LevelsCatalogue _catalogue;

        public LevelsMenu(LevelsCatalogue catalogue)
        {
            _catalogue = catalogue;
        }

        public LevelSettings[] GetLevels()
        {
            return _catalogue.GetLevels();
        }
    }
}
