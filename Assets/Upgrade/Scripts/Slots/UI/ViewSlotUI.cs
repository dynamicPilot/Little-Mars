using LittleMars.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace LittleMars.Slots.UI
{
    public class ViewSlotUI : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private ViewSlotSignUI _signUI;

        private void Awake()
        {
            _canvas.worldCamera = Camera.main;
        }

        public void UpdateSigns(Resource[] resources)
        {
            _signUI.UpdateSigns(resources);
        }

        public void HideSigns()
        {
            _signUI.gameObject.SetActive(false);

        }

        public void ShowSigns()
        {
            _signUI.gameObject.SetActive(true);
        }
    }
}
