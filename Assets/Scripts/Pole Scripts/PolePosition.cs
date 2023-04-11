using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PolePosition : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] [Range(1, 3)] private int _pole_position;
    [SerializeField] private Pole _pole_ref;
    public Pole PolePositionPole
    {
        get { return _pole_ref; }
        set { _pole_ref = value; }
    }

    #region Event Parameters
    private EventParameters poleParams;
    #endregion

    void Start()
    {
        poleParams = new EventParameters();
        poleParams.AddParameter(EventParamKeys.SELECTED_POLE, _pole_ref);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_pole_ref.IsHoveringOver)
            return;

        EventBroadcaster.Instance.PostEvent(EventKeys.POLE_HOVER, poleParams);
        _pole_ref.IsHoveringOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _pole_ref.IsHoveringOver = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        EventBroadcaster.Instance.PostEvent(EventKeys.POLE_PRESS, poleParams);
    }
}
