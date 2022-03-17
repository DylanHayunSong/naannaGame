using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonEX : Button
{
    public UnityEvent buttonDownEvent;
    public UnityEvent buttonUpEvent;
    public UnityEvent buttonReleaseEvent;
    private bool isInButton;

    public override void OnPointerDown (PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        isInButton = true;
        buttonDownEvent.Invoke();
    }

    public override void OnPointerExit (PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        isInButton = false;
    }

    public override void OnPointerUp (PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        if (!isInButton)
        {
            buttonReleaseEvent.Invoke();
        }
        else
        {
            buttonUpEvent.Invoke();
        }
    }
}
