using LittleMars.AudioSystems;
using LittleMars.Common;
using LittleMars.Common.Signals;
using LittleMars.Slots;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.UI.BuildingSlots
{
    /// <summary>
    /// This is a script for Draggable items. 
    /// </summary>
    public class DraggableItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private RectTransform _canvas;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Image _image;

        SignalBus _signalBus;
        BuildingObject _buildingObject;
        UISoundSystem _audioSystem;
        Vector2 _initialLocalObjectPosition;
        Vector2 _initialLocalPointerPosition;

        [Inject]
        public void Constructor(GameUI gameUI, BuildingObject buildingObject, 
            SignalBus signalBus, UISoundSystem audioSystem)
        {
            _canvas = gameUI.MainCanvas;
            _buildingObject = buildingObject;
            _signalBus = signalBus;
            _audioSystem = audioSystem;

        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _initialLocalObjectPosition = _rectTransform.localPosition;

            // set local pointer position to word position
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas,
                eventData.position, eventData.pressEventCamera, out _initialLocalPointerPosition);

            _canvasGroup.blocksRaycasts = false;
            _image.maskable = false;
            OnStartDrag();
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 localPointerPosition;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas,
                eventData.position, eventData.pressEventCamera, out localPointerPosition))
            {
                Vector2 offsetToOriginal = localPointerPosition - _initialLocalPointerPosition;

                _rectTransform.localPosition = _initialLocalObjectPosition + offsetToOriginal;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            RaycastAndGetTarget(eventData.position);
            _rectTransform.localPosition = _initialLocalObjectPosition;
            _canvasGroup.blocksRaycasts = true;
            _image.maskable = true;
            OnEndDrag();
        }

        void RaycastAndGetTarget(Vector2 pointerPosition)
        {
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(pointerPosition);
            RaycastHit2D hit2D = Physics2D.Raycast(worldPosition, Vector2.zero);

            if (hit2D.collider == null) return;

            if (hit2D.collider.gameObject.CompareTag("MapSlot"))
            {
                hit2D.collider.gameObject.GetComponent<IDropTarget>().OnDrop(_buildingObject);
            }
        }

        void OnStartDrag()
        {
            _audioSystem.PlayUISound(UISoundType.zoomIn);
            _signalBus.Fire<BeginBuildingDragSignal>();
        }

        void OnEndDrag()
        {
            _audioSystem.PlayUISound(UISoundType.drop);
        }

    }
}
