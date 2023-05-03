using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PolePosition : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] [Range(-1, 3)] private int _pole_order;
    public int PoleOrder
    {
        get { return _pole_order; }
    }
    [SerializeField] private Pole _pole_ref;
    public Pole PoleRef
    {
        get { return _pole_ref; }
        set { _pole_ref = value; }
    }

    [SerializeField] private BoxCollider _pole_pos_collider;

    #region Event Parameters
    private EventParameters poleParams;
    #endregion

    public void Initialize()
    {
        if(poleParams is null)
            poleParams = new EventParameters();
        if (_pole_pos_collider is null)
            _pole_pos_collider = GetComponent<BoxCollider>();

        poleParams.AddParameter(EventParamKeys.POLE, _pole_ref);
    }
    public Vector3 GetLocation()
    {
        return this.transform.parent.localPosition;
    }
    private bool poleInOrder()
    {
        if (_pole_order < 1)
            return false;
        return true;
    }

    public void TogglePolePosition(bool toggle)
    {
        _pole_pos_collider.enabled = toggle;

    }

    #region Pointer Events
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!poleInOrder())
            return;

        if (_pole_ref.IsHoveringOver)
            return;

        EventBroadcaster.Instance.PostEvent(EventKeys.POS_ENTER, poleParams);
        _pole_ref.IsHoveringOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!poleInOrder())
            return;

        _pole_ref.IsHoveringOver = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!poleInOrder())
            return;

        EventBroadcaster.Instance.PostEvent(EventKeys.POS_PRESS, poleParams);
    }
    #endregion

}
