using LittleMars.UI.Windows;
using LittleMars.WindowManagers;
using UnityEngine;
using Zenject;

namespace LittleMars.UI.Windows
{
    public class GameWindow: MonoBehaviour
    {
        WindowID _id;
        WindowObject _window;

        [Inject]
        public void Contrusctor(WindowID id, WindowObject.Factory factory)
        {
            id = _id;
            _window = factory.Create();
        }

        public class Factory : PlaceholderFactory<WindowID, GameWindow>
        { }
    }
}
