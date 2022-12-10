using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace LittleMars.Slots.Views
{
    /// <summary>
    /// This is a script for Slot View Components.
    /// </summary>
    public class ViewSlotView : MonoBehaviour
    {
        [SerializeField] private GameObject _emptyState;
        [SerializeField] private GameObject _placingState;
        [SerializeField] private GameObject _buildingState;

        [Inject]
        public void Constructor (Vector2 position)
        {
            transform.localPosition = position;
        }

        public Vector2 Position  { 
            get => transform.localPosition;
        }
        public void EmptyState()
        {
            //Debug.Log("Setting slot empty state");
            _emptyState.SetActive(true);
            _placingState.SetActive(false);
            _buildingState.SetActive(false);
        }

        public void PlacingState()
        {
            _emptyState.SetActive(false);
            _placingState.SetActive(true);
        }

        public void BuildingState()
        {
            _placingState.SetActive(false);
            _buildingState.SetActive(true);
        }

    }
}
