using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LittleMars.View.Game.Map
{
    public class MapSlotState: MonoBehaviour
    {
        public bool IsEmpty { get; private set; }

        private void Awake()
        {
            IsEmpty = true;
        }
    }
}
