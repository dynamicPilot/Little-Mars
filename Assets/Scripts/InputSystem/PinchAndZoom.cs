using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchAndZoom : MonoBehaviour
{
    [SerializeField] private RectTransform objectToMakeZoom;

    [Header("Zoom Settings")]
    [SerializeField] private float distanceForOne = 10;
    [SerializeField] private Vector3 minScale;
    [SerializeField] private Vector3 maxScale;

    //float mouseZoomSpeed = 15.0f;
    //float touchZoomSpeed = 0.1f;
    //float zoomMinBound = 0.1f;
    //float zoomMaxBound = 179.9f;

    void Update()
    {
        // Pinch to zoom
        if (Input.touchCount == 2)
        {
            // get current touch positions
            Touch tZero = Input.GetTouch(0);
            Touch tOne = Input.GetTouch(1);

            // get touch position from the previous frame
            Vector2 tZeroPrevious = tZero.position - tZero.deltaPosition;
            Vector2 tOnePrevious = tOne.position - tOne.deltaPosition;

            float oldTouchDistance = Vector2.Distance(tZeroPrevious, tOnePrevious);
            float currentTouchDistance = Vector2.Distance(tZero.position, tOne.position);

            // get offset value
            float deltaDistance = oldTouchDistance - currentTouchDistance;
            //Debug.Log("PinchAndZoom: delta " + deltaDistance + " speed " + touchZoomSpeed);
            Zoom(deltaDistance);
        }
        //if (Input.touchSupported)
        //{
            

        //}
        //else
        //{

        //    float scroll = Input.GetAxis("Mouse ScrollWheel");
        //    //Debug.Log("PinchAndZoom: scroll " + scroll + " speed " + mouseZoomSpeed);
        //    Zoom(scroll, mouseZoomSpeed);
        //}


        //if (cam.fieldOfView < ZoomMinBound)
        //{
        //    cam.fieldOfView = 0.1f;
        //}
        //else
        //if (cam.fieldOfView > ZoomMaxBound)
        //{
        //    cam.fieldOfView = 179.9f;
        //}
    }

    void Zoom(float deltaMagnitudeDiff)
    {
        float actualScaleChange = -deltaMagnitudeDiff / distanceForOne;
        Vector3 currentScale = objectToMakeZoom.localScale;
        Vector3 newScale = new Vector3(currentScale.x + actualScaleChange, currentScale.y + actualScaleChange, currentScale.z + actualScaleChange);

        if (newScale.x < minScale.x || newScale.y < minScale.y)
        {
            newScale = minScale;
        }
        else if (newScale.x > maxScale.x || newScale.y > maxScale.y)
        {
            newScale = maxScale;
        }

        objectToMakeZoom.localScale = newScale;
        //Debug.Log("PinchAndZoom: delta " + deltaMagnitudeDiff + " speed " + speed);
        //cam.fieldOfView += deltaMagnitudeDiff * speed;
        //// set min and max value of Clamp function upon your requirement
        //cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, zoomMinBound, zoomMaxBound);
    }
}
