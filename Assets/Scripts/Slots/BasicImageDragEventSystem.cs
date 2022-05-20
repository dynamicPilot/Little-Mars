using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BasicImageDragEventSystem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private MenuSlot slot;

    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Image image;
    private Vector2 initialPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
        
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        slot.HideInfo();
        image.maskable = false;
        initialPosition = rectTransform.anchoredPosition;
        canvasGroup.blocksRaycasts = false;

        if (slot.GetItem().IconWhenDrag != null)
        {
            image.sprite = slot.GetItem().IconWhenDrag;
        }
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = initialPosition;
        canvasGroup.blocksRaycasts = true;
        image.maskable = true;

        image.sprite = slot.GetItem().Icon;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.pointerCurrentRaycast.screenPosition;
    }


    public BuildingItem GetItem()
    {
        return slot.GetItem();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        slot.ShowInfo();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        slot.HideInfo();
    }
}
