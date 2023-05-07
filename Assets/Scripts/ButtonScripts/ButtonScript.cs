using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] ButtonController _button_ctrler;

    [SerializeField] BoxCollider _button_collider;

    private void Start()
    {
        if (_button_ctrler is null)
            _button_ctrler = GetComponent<ButtonController>();

        if (_button_collider is null)
            _button_collider = GetComponent<BoxCollider>();

    }

    public void ButtonPress(float buttonDefaultPosY, float buttonPressedPosY, float releaseSpeed)
    {
        _button_ctrler.ButtonPressed(buttonDefaultPosY, buttonPressedPosY, releaseSpeed);

    }

    public void ToggleButton(bool toggle)
    {
        _button_collider.enabled = toggle;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        EventBroadcaster.Instance.PostEvent(EventKeys.BUTTON_RESET_CLICKED, null);
    }

}
