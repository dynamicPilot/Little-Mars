using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.UI.BuildingsSlots
{
    public class ResourceScreenControllerUI : MonoBehaviour
    {
        [SerializeField] private Button _button;
        ResourcesScreenController _controller;

        [Inject]
        public void Constructor(ResourcesScreenController controller)
        {
            _controller = controller;
            Init();
        }

        private void Init()
        {
            _button.onClick.AddListener(_controller.ToNextScreen);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}
