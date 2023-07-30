using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class CustomJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform joystickTransform;
    [SerializeField] private float joystickRadius = 100f;

    private Vector2 startPosition;
    private Vector2 joystickDirection;
    private Vector2 touchOffset;
    private bool isDragging = false;

    private void Awake()
    {
        startPosition = joystickTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position = ClampPosition(eventData.position + touchOffset);
        joystickTransform.anchoredPosition = position;
        UpdateJoystickDirection();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        touchOffset = joystickTransform.anchoredPosition - eventData.position;
        joystickTransform.anchoredPosition = eventData.position + touchOffset;
        UpdateJoystickDirection();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        joystickTransform.anchoredPosition = startPosition;
        joystickDirection = Vector2.zero;
    }

    private void UpdateJoystickDirection()
    {
        Vector2 delta = (joystickTransform.anchoredPosition - startPosition) / joystickRadius;
        joystickDirection = delta.magnitude > 1f ? delta.normalized : delta;
    }

    private Vector2 ClampPosition(Vector2 position)
    {
        Vector2 delta = position - startPosition;
        if (delta.magnitude > joystickRadius)
        {
            delta = delta.normalized * joystickRadius;
            position = startPosition + delta;
        }
        return position;
    }

    public Vector2 GetJoystickDirection()
    {
        return joystickDirection;
    }

    public void SnapToTouchPosition(Vector2 touchPosition)
    {
        Vector2 position = ClampPosition(touchPosition + touchOffset);
        joystickTransform.anchoredPosition = position;
        UpdateJoystickDirection();
    }

    public void SnapToInitialPosition()
    {
        joystickTransform.anchoredPosition = startPosition;
        joystickDirection = Vector2.zero;
    }
}