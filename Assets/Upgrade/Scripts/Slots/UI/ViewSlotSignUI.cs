using LittleMars.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UnityEngine;
using Zenject;

namespace LittleMars.Slots.UI
{
    public class ViewSlotSignUI: MonoBehaviour
    {
        SignImage.Factory _signImageFactory;

        List<SignImage> _signs = new List<SignImage>();

        [Inject]
        public void Constructor(SignImage.Factory signImageFactory)
        {
            _signImageFactory = signImageFactory;

        }

        private void CreateSigns()
        {
            var sign = _signImageFactory.Create();
            sign.gameObject.transform.SetParent(gameObject.transform);
            sign.gameObject.transform.localScale = Vector3.one;
            _signs.Add(sign);
        }

        public void UpdateSigns(Resource[] resources)
        {
            if (resources == null) return;

            int i;
            for (i = 0; i < resources.Length; i++)
            {
                if (i > _signs.Count - 1) CreateSigns();
                _signs[i].Resource();
                _signs[i].gameObject.SetActive(true);
            }
            
            if (i < _signs.Count)
            {
                for (int j = i; j < _signs.Count; j++)
                    _signs[j].gameObject.SetActive(false);
            }
        }

    }
}
