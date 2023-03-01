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

        LangManager _manager;
        ILevelLangManager _levelManager;

        [Inject]
        public void Constructor(LangManager manager, ILevelLangManager levelManager)
        {           
            _manager = manager;
            _levelManager = levelManager;
            Init();
        }

        private void Init()
        {
            if (_inInit) SetText();
        }

        public void SetText()
        {
            if (_tagGroup == TagGroup.scene) _text.text = _manager.GetText(_tag, _tagGroup);
            else if (_tagGroup == TagGroup.level) _text.text = _levelManager.GetText(_tag, _tagGroup);
        }
    }
}
