using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LittleMars.View.Game.Map
{
    public class MapSlotControl : MonoBehaviour, IRaycastHitTarget, IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("Pointer down on me");
        }

        public void OnRaycastHitTarget()
        {
            Debug.Log("I was hit");
        }
    }
}
