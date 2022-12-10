using LittleMars.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleMars.View.Game.Map
{
    public class LevelSlotUnits
    {
        public List<List<SlotUnit>> Slots { get; private set; }
        public LevelSlotUnits(List<List<SlotUnit>> slots)
        {
            Slots = slots;
        }

    }
}
