using UnityEngine;

namespace LittleMars.UI.Effects
{
    public class ListScreenEffectUI : MonoBehaviour
    {
        [SerializeField] private GameObject _emptyStateSlot;

        public void IsEmptyState(bool isEmpty)
        {
            _emptyStateSlot.SetActive(isEmpty);
        }

    }
}
