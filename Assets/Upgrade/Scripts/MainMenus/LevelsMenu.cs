using LittleMars.Common.Catalogues;
using LittleMars.Settings;
using System.Collections.Generic;
using Zenject;

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

        public List<bool> GetIsDoneLevels()
        {
            return null;
        }
    }
}
