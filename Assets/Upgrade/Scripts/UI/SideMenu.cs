using LittleMars.Common;
using LittleMars.Common.Signals;

namespace LittleMars.UI
{
    public class SideMenu
    {
        MenuInitType _initType = MenuInitType.resources;

        public SideMenu(MenuInitType initType)
        {
            _initType = initType;
        }

        public virtual void OnNeedInit(NeedMenuInitSignal args)
        {
            if (args.Type != _initType) return;
        }
    }
}
