using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DynamicJoystick : Joystick
{
    public override void OnPointerDown(PointerEventData eventData)
    {
        joystickAnim.SetActive(true);
        animRectTransform.position = eventData.position;
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        joystickAnim.SetActive(false);
    }
}
