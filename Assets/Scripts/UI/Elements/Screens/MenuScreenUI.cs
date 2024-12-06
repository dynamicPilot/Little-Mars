using LittleMars.Common.Signals;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.UI.Elements.MenuScreens
{
    public class MenuScreenUI : MonoBehaviour
    {
        [SerializeField] GameObject _image;
        [SerializeField] TextMeshProUGUI _text;
        [SerializeField] Button _button;

        [Header("Settings")]
        [SerializeField] float _showPeriod = 5f;

        bool _coroutineIsActive = false;
        Coroutine _coroutine;
        SignalBus _signalBus;

        [Inject]
        public void Constructor(SignalBus signalBus)
        {
            _signalBus = signalBus;
            _coroutineIsActive = false;
            Init();
        }

        void Init()
        {
            _button.onClick.AddListener(SetImage);
            _signalBus.Subscribe<MenuScreenSetText>(OnMenuScreenSetText);
        }

        private void OnDestroy()
        {
            StopActiveCoroutine();
            _button.onClick?.RemoveListener(SetImage);
            _signalBus?.TryUnsubscribe<MenuScreenSetText>(OnMenuScreenSetText);
        }

        void OnMenuScreenSetText(MenuScreenSetText args)
        {
            if (args.SameId)
            {
                // the same id
                if (_image.activeSelf) SetText(args.Text);
                else SetImage();
            }
            else SetText(args.Text);
        }

        void SetText(string text)
        {
            StopActiveCoroutine();
            if (!_text.gameObject.activeSelf)
                _text.gameObject.SetActive(true);

            if (_image.activeSelf)
                _image.SetActive(false);

            _text.SetText(text);
            _coroutine = StartCoroutine(WaitToImage());
        }

        void SetImage()
        {
            StopActiveCoroutine();

            if (_text.gameObject.activeSelf)
                _text.gameObject.SetActive(false);

            if (!_image.activeSelf)
                _image.SetActive(true);
        }

        void StopActiveCoroutine()
        {
            if (!_coroutineIsActive) return;

            StopCoroutine(_coroutine);
            _coroutineIsActive = false;
        }

        IEnumerator WaitToImage()
        {
            _coroutineIsActive = true;
            yield return new WaitForSecondsRealtime(_showPeriod);
            SetImage();
            _coroutineIsActive = false;
        }
    }
}
