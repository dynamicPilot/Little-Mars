using LittleMars.Common.Signals;
using System;
using Zenject;

namespace LittleMars.UI.Elements.MenuScreens
{
    public class MenuScreenController
    {
        readonly SignalBus _signalBus;
        int _id;
        bool _sameId;
        MenuScreenSetText _menuScreenSetText;

        public MenuScreenController(SignalBus signalBus)
        {
            _signalBus = signalBus;
            _id = -1;
            _sameId = false;
        }

        public void CallText(string text, int id)
        {
            _menuScreenSetText.Text = text;
            _menuScreenSetText.SameId = _id == id;
            _signalBus.Fire(_menuScreenSetText);

            _id = id;
        }
    }
}
