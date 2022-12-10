using LittleMars.View.Spawners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace LittleMars.View.Game.Map
{
    public class SlotsSystem : MonoBehaviour
    {
        private LevelSlotUnits _slots;

        [Inject]
        public void Constructor(ISpawner<LevelSlotUnits> spawner, SceneData data)
        {
            _slots = spawner.Spawn(data.SlotPrefab, data.SlotParent);
        }
    }
}
