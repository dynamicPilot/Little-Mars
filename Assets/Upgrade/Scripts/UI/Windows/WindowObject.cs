using UnityEngine;
using Zenject;

namespace LittleMars.UI.Windows
{
    public class WindowObject : MonoBehaviour
    {

        public class Factory : PlaceholderFactory<WindowObject>
        { }
    }
}
