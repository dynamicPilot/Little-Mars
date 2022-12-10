using LittleMars.Models.Grid;
using LittleMars.View.Game.Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LittleMars.View.Spawners
{
    public interface ISpawner<T>
    {
        T Spawn(GameObject prefab, Transform parent);

    }
    public class SlotSpawner : ISpawner<LevelSlotUnits>
    {
        LevelGrids _levelGrids;

        public SlotSpawner(LevelGrids levelGrids)
        {
            _levelGrids = levelGrids;
        }

        public LevelSlotUnits Spawn(GameObject prefab, Transform parent)
        {
            Debug.Log("Spawn slots");
            List<List<SlotUnit>> slots = new List<List<SlotUnit>>();
            for (int i = 0; i < _levelGrids.Rows; i++)
            {
                List<SlotUnit> row = new List<SlotUnit>();
                for (int j = 0; j < _levelGrids.Columns; j++)
                {
                    var slot = GameObject.Instantiate(prefab, parent).GetComponent<SlotUnit>();
                    slot.transform.localPosition = _levelGrids.Coordinates[i][j];
                    slot.SetSlotUnit(i, j, _levelGrids.GridUnits[i][j]);
                    row.Add(slot);
                }
                slots.Add(row);
            }

            return new LevelSlotUnits(slots);
        }
    }
}

