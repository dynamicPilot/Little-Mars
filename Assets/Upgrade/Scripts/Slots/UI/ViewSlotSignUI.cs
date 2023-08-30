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

        public void UpdateSigns(Resource[] resources, bool isBlocked)
        {
            if (resources == null && !isBlocked) return;

            int index = 0;

            if (isBlocked)
            {
                // create isBlocked Icon
                CreateIsBlockedSlot(ref index);
            }

            if (resources != null && resources.Length > 0)
                CreateResourcesSlots(resources, ref index);

            HideOtherSlots(index);
        }
        void CreateIsBlockedSlot(ref int index)
        {
            if (index > _signs.Count - 1) CreateSigns();
            _signs[index].IsBlocked();
            _signs[index].gameObject.SetActive(true);
            index++;
        }

        void CreateResourcesSlots(Resource[] resources, ref int index)
        {
            for (; index < resources.Length; index++)
            {
                if (index > _signs.Count - 1) CreateSigns();
                _signs[index].Resource(resources[index]);
                _signs[index].gameObject.SetActive(true);
            }
        }

        void HideOtherSlots(int index)
        {
            if (index < _signs.Count)
            {
                for (int j = index; j < _signs.Count; j++)
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
