using UnityEngine;

namespace LittleMars.UI.WindowManagers
{
    public class WindowManagerUI: MonoBehaviour
    {
        [SerializeField] private GameObject _testMenuPrefab;
        public GameObject GetWindowPrefab()
        {
            return _testMenuPrefab;
        }
    }
}
