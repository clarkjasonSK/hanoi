using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour, IPointerUpHandler
{
    [SerializeField] Slider _ring_slider;

    #region Event Parameters
    private EventParameters _ui_param;
    #endregion

    void Start()
    {
        _ui_param = new EventParameters();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!_ring_slider.interactable)
            return;

        _ui_param.AddParameter<int>(EventParamKeys.SLIDER_NUMBER, (int)_ring_slider.value);

        EventBroadcaster.Instance.PostEvent(EventKeys.SLIDER_CHANGE, _ui_param);
    }

    public void ToggleSlider(bool toggle)
    {
        _ring_slider.interactable = toggle;
    }
}
