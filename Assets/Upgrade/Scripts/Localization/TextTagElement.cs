using LittleMars.Common;
using TMPro;
using UnityEngine;
using Zenject;

namespace LittleMars.Localization
{
    public class TextTagElement : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private TagGroup _tagGroup;
        [SerializeField] private TextMeshProUGUI _text;

        [Header("Options")]
        [SerializeField] private bool _inInit = false;

        LangsManager _manager;

        [Inject]
        public void Constructor(LangsManager manager)
        {
            _manager = manager;
            Init();
        }

        private void Init()
        {
            if (_inInit) SetText();
        }

        public void SetText()
        {
            _text.text = _manager.GetText(_tag, _tagGroup);
        }
    }
}
