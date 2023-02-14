using LittleMars.Common;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LittleMars.Slots.UI
{
    public class ViewSlotSignUI: MonoBehaviour
    {
        SignImage.Factory _signImageFactory;
        List<SignImage> _signs;

        [Inject]
        public void Constructor(SignImage.Factory signImageFactory)
        {
            _signImageFactory = signImageFactory;
            _signs = new List<SignImage>();
        }

        public void UpdateSigns(Resource[] resources)
        {
            if (resources == null) return;

            int i;
            for (i = 0; i < resources.Length; i++)
            {
                if (i > _signs.Count - 1) CreateSigns();
                _signs[i].Resource(resources[i]);
                _signs[i].gameObject.SetActive(true);
            }
            
            if (i < _signs.Count)
            {
                for (int j = i; j < _signs.Count; j++)
                    _signs[j].gameObject.SetActive(false);
            }
        }

        void CreateSigns()
        {
            var sign = _signImageFactory.Create();
            sign.gameObject.transform.SetParent(gameObject.transform);
            sign.gameObject.transform.localScale = Vector3.one;
            _signs.Add(sign);
        }

    }
}
