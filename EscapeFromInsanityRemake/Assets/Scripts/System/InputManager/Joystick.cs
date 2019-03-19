using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler , IDragHandler , IPointerUpHandler
{
    public MobileInputManager inputManager;

    public void OnDrag(PointerEventData eventData)
    {
        inputManager.SetPosition(eventData.position, GetComponent<RectTransform>());
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        inputManager.SetPosition(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputManager.ResetPosition();
    }
}
