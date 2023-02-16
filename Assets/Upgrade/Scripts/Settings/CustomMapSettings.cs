using LittleMars.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace LittleMars.Settings
{
    [CreateAssetMenu(menuName = "LittleMars/Settings/Custom Map Settings")]
    public class CustomMapSettings : ScriptableObject//Installer<CustomMapSettings>
    {
        public int Rows;
        public int Columns;
        public MapLine[] Lines;
    }
}
