using LittleMars.Common;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LittleMars.Connections
{
    public class SlotConnections
    {
        public Indexes Indexes { get; private set; }
        Dictionary<Direction, SlotConnections> _neighbors;
        Dictionary<Direction, Connection> _connections;
        public Dictionary<Direction, Connection> Connections { get => _connections; }

        List<BuildingType> _needed;
        BuildingType _provided;

        [Inject]
        public void Initialize()
        {
            _neighbors = new Dictionary<Direction, SlotConnections>();
            _connections = new Dictionary<Direction, Connection>();
            _needed = new List<BuildingType>();
            _provided = BuildingType.none;

            FillConnections();
        }

        public void SetIndexes(int row, int col)
        {
            Indexes = new Indexes(row, col);
        }

        public void AddNeighbor(Direction direction, SlotConnections neighbor)
        {
            _neighbors.Add(direction, neighbor);
        }

        public SlotConnections[] GetNeighbors()
        {
            var neighbors = new List<SlotConnections>();

            foreach (Direction direction in _neighbors.Keys)
                neighbors.Add(_neighbors[direction]);

            return neighbors.ToArray();
        }

        public void AddBuilding(BuildingType type, BuildingType[] connectionsNeeded)
        {
            Debug.Log("Add building to the connection slot");
            _provided= type;
            _needed.AddRange(connectionsNeeded);

            // add connection with off state
            SetConnections();
        }

        public void RemoveBilding()
        {
            _provided = BuildingType.none;
            _needed.Clear();

            // connections to null
            RemoveConnections();
        }

        public bool TryAddConnection(Direction direction, BuildingType type)
        {
            // needed
            if (!_needed.Contains(type)) return false;

            AddConnection(direction, type);
            return true;
        }

        public bool TryGetConnection(Direction direction, BuildingType type)
        {
            // provided
            if (_provided != type) return false;

            AddConnection(direction, type);
            return true;
        }

        public void RemoveConnection(Direction direction)
        {
            _connections[direction] = null;

            // update connection in scene
        }

        public List<BuildingType> GetActiveConnections()
        {
            List<BuildingType> connections = new List<BuildingType>();

            foreach(var direction in _connections.Keys)
            {
                if (_connections[direction] == null) continue;
                else if (_connections[direction].Type == _provided) continue;
                else if (connections.Contains(_connections[direction].Type)) continue;

                connections.Add(_connections[direction].Type);
            }

            return connections;
        }

        private void SetConnections()
        {
            Debug.Log("SET SLOT CONNECTIONS FOR " + _provided);
            foreach(Direction direction in _neighbors.Keys)
            {
                Debug.Log("Try add provided connection to neighbor " + direction);
                var oppDirection = GetOppositeDirection(direction);
                Debug.Log("...... opposite direction " + oppDirection);
                // provided by self
                bool needAdd = _neighbors[direction].TryAddConnection(oppDirection, _provided);
                Debug.Log("...... need add to self when need for neighbour? " + needAdd);
                if (needAdd) AddConnection(direction, _provided);

                // needed to self
                for (int i = 0; i < _needed.Count; i++)
                {
                    Debug.Log("Try get connection from neighbor " + _needed[i].ToString());
                    needAdd = _neighbors[direction].TryGetConnection(oppDirection, _needed[i]);
                    Debug.Log("...... need add to self? " + needAdd);
                    if (needAdd) AddConnection(direction, _needed[i]);
                }
            }
        }

        private void RemoveConnections()
        {
            foreach(Direction direction in _neighbors.Keys)
            {
                if (_connections[direction] == null) continue;

                var oppDirection = GetOppositeDirection(direction);
                _neighbors[direction].RemoveConnection(oppDirection);
                RemoveConnection(direction);
            }
        }

        private void AddConnection(Direction direction, BuildingType type)
        {
            Debug.Log("...... add connection of " + type + " to " + _provided + " to direction " + direction);
            _connections[direction] = new Connection(type, States.on);

            // update connection in scene
        }

        void UpdateConnectionView()
        {

        }

        private Direction GetOppositeDirection(Direction direction)
        {
            var index = ((int)direction + 2) % 4;
            return (Direction)index;
        }


        private void FillConnections()
        {
            _connections.Add(Direction.left, null);
            _connections.Add(Direction.up, null);
            _connections.Add(Direction.right, null);
            _connections.Add(Direction.down, null);
        }

        public class Factory : PlaceholderFactory<SlotConnections>
        { }
    }
}
