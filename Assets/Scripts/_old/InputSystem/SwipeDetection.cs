using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField] private InputControl inputControl;

    [Header("Settings")]
    [SerializeField] private float minDistance = 0.2f;
    [SerializeField] private float maxTime = 1f;
    [SerializeField, Range (0f, 1f)] private float directionThreshold = 0.9f;

    public delegate void SwipeToHorizontal(int direction);
    public event SwipeToHorizontal OnSwipeToHorizontal;

    private Vector2 startPosition;
    private float startTime;

    private Vector2 endPosition;
    private float endTime;

    private void OnEnable()
    {
        inputControl.OnStartTouch += SwipeStart;
        inputControl.OnEndTouch += SwipeEnd;
    }

    private void OnDisable()
    {
        inputControl.OnStartTouch -= SwipeStart;
        inputControl.OnEndTouch -= SwipeEnd;
    }

    private void SwipeStart(Vector2 position, float time)
    {
        startPosition = position;
        startTime = time;

    }

    private void SwipeEnd(Vector2 position, float time)
    {
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Vector3.Distance(startPosition, endPosition) >= minDistance && (endTime - startTime <= maxTime))
        {
            //Debug.Log("SwipeDetection: SWIPE");
            //Debug.DrawLine(startPosition, endPosition, Color.red, 5f);
            Vector3 direction3D = endPosition - startPosition;
            Vector2 direction = new Vector2(direction3D.x, direction3D.y).normalized;
            SwipeDirection(direction);
        }
    }

    private void SwipeDirection(Vector2 direction)
    {
        if (Vector2.Dot(Vector2.up, direction) > directionThreshold)
        {
            // up
        }
        else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
        {
            // down
        }
        else if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
        {
            // left
            Debug.Log("SwipeDetection: left swipe");
            if (OnSwipeToHorizontal != null)
            {
                OnSwipeToHorizontal.Invoke(1);
            }
        }
        else if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
        {
            // right
            Debug.Log("SwipeDetection: right swipe");
            if (OnSwipeToHorizontal != null)
            {
                OnSwipeToHorizontal.Invoke(-1);
            }
        }
    }

}
