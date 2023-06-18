using LittleMars.Common;
using LittleMars.WindowManagers;
using System.Collections.Generic;
using UnityEngine;

namespace LittleMars.UI.Windows
{
    /// <summary>
    /// This Script keeps all open in start windows in the scene.
    /// </summary>
    public class SceneWindows : MonoBehaviour
    {
        //[SerializeField] WindowControlUnit<GameWindow>[] _windows;
        [SerializeField] RectTransform _canvas;
        [SerializeField] WindowID[] _startWindows;

        public RectTransform Canvas { get => _canvas; }
        public WindowID[] StartWindows { get => _startWindows; }

        //public Dictionary<int, GameWindow> GetOpenedWindows()
        //{
        //    if (_windows.Length == 0) return new Dictionary<int, GameWindow>();
        //    else return FormWindowsDict();
        //}

        //Dictionary<int, GameWindow> FormWindowsDict()
        //{
        //    var dict = new Dictionary<int, GameWindow>();
        //    for (int i =0; i < _windows.Length; i++)
        //        dict.Add((int) _windows[i].WindowID, _windows[i].Unit);

        //    return dict;
        //}
    }
}
