using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] ButtonController _button_ctrler;

    [SerializeField] BoxCollider _button_collider;

    #region SFX 
    [SerializeField] AudioSource _audio_src;
    [SerializeField] SimpleSFX _button_pressed_sfx;
    #endregion

    private void Start()
    {
        if (_button_ctrler is null)
            _button_ctrler = GetComponent<ButtonController>();

        if (_button_collider is null)
            _button_collider = GetComponent<BoxCollider>();

        if (_audio_src is null)
            _audio_src = GetComponent<AudioSource>();

    }

    public void ButtonPress(float buttonDefaultPosY, float buttonPressedPosY, float releaseSpeed)
    {
        _button_pressed_sfx.PlaySFX(_audio_src);
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
