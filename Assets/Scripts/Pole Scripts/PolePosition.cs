using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PolePosition : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] [Range(-1, 3)] private int _pole_order;
    [SerializeField] private Pole _pole_ref;
    public Pole PoleRef
    {
        get { return _pole_ref; }
        set { _pole_ref = value; }
    }

    #region Event Parameters
    private EventParameters poleParams;
    #endregion

    void Start()
    {
        if (_pole_order < 1)
            return;

        poleParams = new EventParameters();
        poleParams.AddParameter(EventParamKeys.SELECTED_POLE, _pole_ref);
    }

    public Vector3 GetLocation()
    {
        return this.transform.parent.localPosition;
    }

    #region Pointer Events
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
    #endregion
}
