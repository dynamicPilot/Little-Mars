using LittleMars.Common;
using LittleMars.Common.Catalogues;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LittleMars.Connections.View
{
    public class ConnectionIndicators : MonoBehaviour
    {
        [SerializeField] ConnectionIndicator _left;
        [SerializeField] ConnectionIndicator _up;
        [SerializeField] ConnectionIndicator _right;
        [SerializeField] ConnectionIndicator _down;

        Dictionary<Direction, ConnectionIndicator> _indicators = null;
        ColorsCatalogue _colors;

        bool _canShow = false;

        [Inject]
        public void Constructor(ColorsCatalogue colors)
        {
            _colors = colors;
        }

        public void UpdateIndicators(Dictionary<Direction, Connection> connections)
        {
            Debug.Log("Update indicators for building");
            //if (!_canShow)
            //{
            //    Debug.Log("... can not show");
            //    return;
            //}
            //else
            if (_indicators == null) CreateIndicatorsDictionary();

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
            Debug.Log("Hide indicator");
            if (_indicators == null) return;

            foreach (Direction direction in _indicators.Keys)
            {
                HideIndicator(direction);
            }
        }

        private void CreateIndicatorsDictionary()
        {
            _indicators = new Dictionary<Direction, ConnectionIndicator>
            {
                { Direction.left, _left },
                { Direction.up, _up },
                { Direction.right, _right },
                { Direction.down, _down }
            };
        }

        private void HideIndicator(Direction direction)
        {
            if (!_indicators[direction].gameObject.activeSelf) return;

            _indicators[direction].gameObject.SetActive(false);
        }

        private void UpdateIndicator(Direction direction, BuildingType type)
        {
            Debug.Log("Update indicator for direction " + direction + " of type " + type);
            if (!_indicators[direction].gameObject.activeSelf) 
                _indicators[direction].gameObject.SetActive(true);

            // change color
            // get colot of type
            var color = _colors.ConnectionColor(type);
            _indicators[direction].SetIndicator(color);
        }
    }
}
