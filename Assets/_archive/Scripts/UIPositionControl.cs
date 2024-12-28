using UnityEngine;

public class UIPositionControl : MonoBehaviour
{
    [SerializeField] private bool isMobile = true;

    [Header("UI to Control")]
    [SerializeField] private RectTransform resourcesPanel;
    [SerializeField] private float nativePosX = 92f;
    [SerializeField] private float mobilePosX = 142f;

    private void OnEnable()
    {
        if (isMobile)
        {
            // move UI
            resourcesPanel.anchoredPosition = new Vector2(mobilePosX, resourcesPanel.anchoredPosition.y);
        }
        else
        {
            resourcesPanel.anchoredPosition = new Vector2(nativePosX, resourcesPanel.anchoredPosition.y);
        }
    }
}
