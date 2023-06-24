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
        [SerializeField] RectTransform _canvas;
        [SerializeField] WindowID[] _startWindows;

        public RectTransform Canvas { get => _canvas; }
        public WindowID[] StartWindows { get => _startWindows; }
    }
}
