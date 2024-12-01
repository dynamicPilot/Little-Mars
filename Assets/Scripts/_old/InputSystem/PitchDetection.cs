using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchDetection : MonoBehaviour
{
    
    [SerializeField] private GameObject buttonsPanel;

    [Header("Settings")]
    [SerializeField] private bool needSetEventsForTouch = true;
    [SerializeField] private bool needZoomButtons = false;
    [SerializeField] private RectTransform objectToMakeZoom;
    [SerializeField] private float zoomSpeed = 10;
    [SerializeField] private Vector3 minScale;
    [SerializeField] private Vector3 maxScale;

    [Header("Scripts")]
    [SerializeField] private MapControl mapControl;
    [SerializeField] private InputControl inputControl;
    //private TouchControls controls;
    private Coroutine zoomCoroutine;

    //private void Awake()
    //{
    //    //controls = new TouchControls();
    //}

    private void OnEnable()
    {
        //controls.Enable();
        buttonsPanel.SetActive(needZoomButtons);
        if (needSetEventsForTouch)
        {
            inputControl.OnStartPitch += ZoomStart;
            inputControl.OnEndPitch += ZoomStop;
        }
        
    }

    private void OnDisable()
    {
        //controls.Disable();
        try
        {
            buttonsPanel.SetActive(needZoomButtons);
            if (needSetEventsForTouch)
            {
                inputControl.OnStartPitch -= ZoomStart;
                inputControl.OnEndPitch -= ZoomStop;
            }
        }
        catch
        {

        }

    }

    //private void Start()
    //{
    //    //controls.Touch.SecondaryTouchContact.started += ctx => ZoomStart();
    //    //controls.Touch.SecondaryTouchContact.canceled += ctx => ZoomStop();

    //}

    private void ZoomStart(float distance)
    {
        zoomCoroutine = StartCoroutine(ZoomDetection(distance));
    }

    private void ZoomStop()
    {
        StopCoroutine(zoomCoroutine);
    }

    IEnumerator ZoomDetection(float distance)
    {
        float prevDistance = 0f;

        while (true)
        {
            //distance = Vector2.Distance(controls.Touch.PrimaryFingerPosition.ReadValue<Vector2>(), controls.Touch.SecondaryFingerPosition.ReadValue<Vector2>());

            // detection
            //if (Vector2.Dot(primaryDelta, secondaryDelta) < -0.9f)

            if (distance > prevDistance)
            {
                // zoom out
                Debug.Log("ZOOM OUT!");
                Zoom(-1 * zoomSpeed);

            }
            else if (distance < prevDistance)
            {
                // zoom in
                Debug.Log("ZOOM IN!");
                Zoom(1 * zoomSpeed);
            }

            prevDistance = distance;
            yield return null;
        }
    }

    public void ZoomByButtons(int direction)
    {
        Zoom(direction * zoomSpeed);
    }

    void Zoom(float delta)
    {
        Vector3 currentScale = objectToMakeZoom.localScale;
        //float[] currentSlidersPosition = mapControl.GetSlidersValue();
        Vector3 newScale = new Vector3(currentScale.x + delta, currentScale.y + delta, currentScale.z + delta);

        if (newScale.x < minScale.x || newScale.y < minScale.y)
        {
            newScale = minScale;
        }
        else if (newScale.x > maxScale.x || newScale.y > maxScale.y)
        {
            newScale = maxScale;
        }

        objectToMakeZoom.localScale = newScale;
        //mapControl.SetSlidersValue(currentSlidersPosition);
        //Debug.Log("PinchAndZoom: delta " + deltaMagnitudeDiff + " speed " + speed);
        //cam.fieldOfView += deltaMagnitudeDiff * speed;
        //// set min and max value of Clamp function upon your requirement
        //cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, zoomMinBound, zoomMaxBound);
    }
}
