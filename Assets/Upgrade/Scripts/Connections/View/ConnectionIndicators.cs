using LittleMars.Common;
using System.Collections.Generic;
using UnityEngine;

namespace LittleMars.Connections.View
{
    public class ConnectionIndicators : MonoBehaviour
    {
        [SerializeField] private ConnectionIndicator _left;
        [SerializeField] private ConnectionIndicator _up;
        [SerializeField] private ConnectionIndicator _right;
        [SerializeField] private ConnectionIndicator _down;

        Dictionary<Direction, ConnectionIndicator> _indicators = null;
        bool _canShow = false;

        public void UpdateIndicators(Dictionary<Direction, Connection> connections)
        {
            if (!_canShow) return;
            else if (_indicators == null) CreateIndicatorsDictionary();

            // update indicators
            foreach(Direction direction in connections.Keys)
            {
                if (connections[direction] == null) HideIndicator(direction);
                else UpdateIndicator(direction, connections[direction].Type);
            }
        }

        public void ChangeCanShowState(bool state)
        {
            _canShow = state;
        }

        public void HideIndicators()
        {
            if (_indicators == null) return;

            foreach (Direction direction in _indicators.Keys)
            {
                HideIndicator(direction);
            }
        }

        private void CreateIndicatorsDictionary()
        {
            _indicators = new Dictionary<Direction, ConnectionIndicator>();

            _indicators.Add(Direction.left, _left);
            _indicators.Add(Direction.up, _up);
            _indicators.Add(Direction.right, _right);
            _indicators.Add(Direction.down, _down);
        }

        private void HideIndicator(Direction direction)
        {
            if (!_indicators[direction].gameObject.activeSelf) return;

            _indicators[direction].gameObject.SetActive(false);
        }

        private void UpdateIndicator(Direction direction, BuildingType type)
        {
            if (!_indicators[direction].gameObject.activeSelf) 
                _indicators[direction].gameObject.SetActive(true);

            _indicators[direction].SetIndicator(Color.cyan);
        }
    }
}
