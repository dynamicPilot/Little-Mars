using LittleMars.Common;
using LittleMars.Map;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LittleMars.Model
{
    /// <summary>
    /// Class for building info when placing building on map.
    /// </summary>
    public class PlacingBuilding
    {
        readonly BuildingType _type;
        readonly Size _size;
        readonly Path _path;
        readonly Vector2 _position;

        int _rotationCount;        
        List<Indexes> _slotIndexes;

        public int RotationCount { get => _rotationCount; set => _rotationCount = value; }
        public List<Indexes> SlotIndexes { get => _slotIndexes; set => _slotIndexes = value; }
        public BuildingType Type { get => _type; }
        public Size Size { get => _size; }
        public Path Path { get => _path; }
        public Vector2 Position { get => _position; }

        public PlacingBuilding(BuildingType type, Size size, Path path, Vector2 position)
        {
            _type = type;
            _size = size;
            _path = path;
            _position = position;
        }

        public Indexes StartSlotIndexes()
        {
            return _slotIndexes[0];
        }

        public class Factory : PlaceholderFactory<BuildingType, Size, Path, Vector2, PlacingBuilding>
        {

        }

    }
}
