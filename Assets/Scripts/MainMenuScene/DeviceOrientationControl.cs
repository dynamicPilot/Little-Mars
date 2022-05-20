using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceOrientationControl : MonoBehaviour
{
    [SerializeField] private ScreenOrientation orientation;
    public ScreenOrientation Orientation { get { return orientation; } }
    [Header("Setting")]
    [SerializeField] private bool needOrientationControl;
    [SerializeField] private float updateInterval = 0.02f;
    //[SerializeField] private ScreenOrientation startScreenOrientation = ScreenOrientation.Portrait;

    public delegate void OrientationChange();
    public event OrientationChange OnOrientationChange;

    private DeviceOrientation prevOrientation;
    private Coroutine deviceOrientationChangeDetector;
    private void Start()
    {
        //StartOrientationChangeDetection();
        SetStartOrientation();
    }

    void SetStartOrientation()
    {
        Debug.Log("DeviceOrientationControl: current DEVICE orientation " + Input.deviceOrientation 
            + "\n                     current SCREEN orientation " + Screen.orientation);
        //Screen.orientation = startScreenOrientation;
    }

    void StartOrientationChangeDetection()
    {
        deviceOrientationChangeDetector = StartCoroutine(DeviceOrientationChangeDetector());
    }

    void StopOrientationChangeDetection()
    {
        StopCoroutine(deviceOrientationChangeDetector);
    }

    IEnumerator DeviceOrientationChangeDetector()
    {
        if (Input.deviceOrientation == DeviceOrientation.Unknown || Input.deviceOrientation == DeviceOrientation.FaceDown || Input.deviceOrientation == DeviceOrientation.FaceUp)
        {
            yield return updateInterval;
        }

        prevOrientation = Input.deviceOrientation;

        while (true)
        {
            if (Input.deviceOrientation != DeviceOrientation.Unknown || Input.deviceOrientation != DeviceOrientation.FaceDown || Input.deviceOrientation != DeviceOrientation.FaceUp)
            {
                if (prevOrientation != Input.deviceOrientation)
                {
                    prevOrientation = Input.deviceOrientation;
                    if (OnOrientationChange != null)
                    {
                        OnOrientationChange.Invoke();
                    }
                }
            }

            yield return updateInterval;
        }
    }
}
