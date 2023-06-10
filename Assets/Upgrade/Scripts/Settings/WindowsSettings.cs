using LittleMars.WindowManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LittleMars.Settings
{
    [CreateAssetMenu(menuName = "LittleMars/Catalogue/WindowCatalogue")]
    public class WindowsSettings : ScriptableObject
    {
        [SerializeField] private GameObject _testWindow;


        public GameObject GetWindowPrefabByID(WindowID id)
        {
            return _testWindow;
        }
    }


}
