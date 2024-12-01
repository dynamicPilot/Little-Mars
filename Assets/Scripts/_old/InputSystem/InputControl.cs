using UnityEngine;
using UnityEngine.InputSystem;

public class InputControl : MonoBehaviour
{
    private TouchControls controls;
    private Camera mainCamera;

    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;

    public delegate void StartPitch(float distance);
    public event StartPitch OnStartPitch;
    public delegate void EndPitch();
    public event EndPitch OnEndPitch;

    //public delegate void ScrollWheelPerformed(float distance);
    //public event ScrollWheelPerformed OnScrollWheelPerformed;

    private void Awake()
    {
        controls = new TouchControls();
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Start()
    {
        controls.UI.PrimaryTouchContact.started += ctx => StartTouchPrimary(ctx);
        controls.UI.PrimaryTouchContact.canceled += ctx => EndTouchPrimary(ctx);

        controls.UI.SecondaryTouchContact.started += ctx => PitchStart();
        controls.UI.SecondaryTouchContact.canceled += ctx => PitchStop();

        //controls.UI.ScrollWheelYDirection.performed += ctx => MouseScroll(); 
    }

    //private void MouseScroll()
    //{
    //    if (OnScrollWheelPerformed != null)
    //    {
    //        OnScrollWheelPerformed.Invoke(controls.UI.ScrollWheelYDirection.ReadValue<Vector2>().y);
    //    }
    //}

    private void PitchStart()
    {
        if (OnStartPitch != null)
        {
            OnStartPitch.Invoke(Vector2.Distance(controls.UI.PrimaryFingerPosition.ReadValue<Vector2>(), controls.UI.SecondaryFingerPosition.ReadValue<Vector2>()));
        }
    }

    private void PitchStop()
    {
        if (OnEndPitch != null)
        {
            OnEndPitch.Invoke();
        }
    }

    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnStartTouch != null)
        {
            OnStartTouch.Invoke(CustomFunctions.ScreenToWorld(mainCamera, controls.UI.PrimaryTouchPosition.ReadValue<Vector2>()),
                (float) context.startTime);
        }
    }

    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnEndTouch != null)
        {
            OnEndTouch.Invoke(CustomFunctions.ScreenToWorld(mainCamera, controls.UI.PrimaryTouchPosition.ReadValue<Vector2>()),
                (float)context.startTime);
        }
    }

    public Vector2 PrimaryPosition()
    {
        return CustomFunctions.ScreenToWorld(mainCamera, controls.UI.PrimaryTouchPosition.ReadValue<Vector2>());
    }
}
