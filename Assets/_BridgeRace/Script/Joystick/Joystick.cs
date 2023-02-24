using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Joystick :
    MonoBehaviour,
    IDragHandler,
    IPointerDownHandler,
    IPointerUpHandler
{
    [SerializeField] protected GameObject joystickAnim;
    [SerializeField] protected Image coverImage;
    [SerializeField] protected Image handleImage;
    [SerializeField] protected Transform controlTargetTransform; 
    
    protected IMovable controlTarget;

    protected RectTransform animRectTransform;
    protected Vector2 recentOffset;

    private void Awake()
    {
        animRectTransform = joystickAnim.GetComponent<RectTransform>();
        controlTarget = controlTargetTransform.GetComponent<IMovable>();
        recentOffset = Vector2.zero;
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        Vector2? inputPosition = GetInputPosition(eventData);
        if (inputPosition == null)
        {
            return;
        }

        Vector2 offset = Normalized(inputPosition.Value);
        MoveHandle(offset);

        Vector3 direction = offset.x * Vector3.right + offset.y * Vector3.forward;
        controlTarget.Move(direction, offset.magnitude);
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        handleImage.rectTransform.anchoredPosition = Vector2.zero;

        controlTarget.StopMoving();
    }

    private Vector2? GetInputPosition(PointerEventData eventData)
    {
        if (
            RectTransformUtility.ScreenPointToLocalPointInRectangle
            (
                animRectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out Vector2 inputPosition
            )
        )
        {
            return inputPosition;
        }
        return null;
    }
    private Vector2 Normalized(Vector2 direction)
    {
        direction.x /= coverImage.rectTransform.sizeDelta.x;
        direction.y /= coverImage.rectTransform.sizeDelta.y;
        if (direction.sqrMagnitude > 1f)
        {
            return direction.normalized;
        }
        return direction;
    }
    protected virtual void MoveHandle(Vector2 offset)
    {
        handleImage.rectTransform.anchoredPosition = new Vector2(
            offset.x * coverImage.rectTransform.sizeDelta.x,
            offset.y * coverImage.rectTransform.sizeDelta.y
        );
    }
}
