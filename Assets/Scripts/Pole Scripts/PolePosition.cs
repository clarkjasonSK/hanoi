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
    [SerializeField] private PolePosition _prev_pole_pos;
    [SerializeField] private PolePosition _next_pole_pos;
    [SerializeField] private Pole _pole_ref;
    public Pole PoleRef
    {
        get { return _pole_ref; }
        set { _pole_ref = value; }
    }

    #region Event Parameters
    private EventParameters poleParams;
    #endregion

    void Awake()
    {
        if (!poleInOrder())
            return;

    }
    public void SetPoleParam()
    {
        if(poleParams is null)
            poleParams = new EventParameters();

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
    #region Pointer Events
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!poleInOrder())
            return;

        if (_pole_ref.IsHoveringOver)
            return;

        EventBroadcaster.Instance.PostEvent(EventKeys.POLE_HOVER, poleParams);
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

        EventBroadcaster.Instance.PostEvent(EventKeys.POLE_PRESS, poleParams);
    }
    #endregion

    public void OnTriggerEnter(Collider col)
    {
        if (!col.gameObject.CompareTag(TagNames.POLE))
            return;
        if (_pole_order != 3)
            return;

        EventBroadcaster.Instance.PostEvent(EventKeys.POS_END_ENTER, null);
    }
    public void OnTriggerExit(Collider col)
    {
        /*
        if (_prev_pole_pos is null && _next_pole_pos is null)
        {
            // spawn pole on top, no movement
            return;
        }
        // if next pole position is null, create 

        EventBroadcaster.Instance.PostEvent(EventKeys.POS_EXIT, poleParams);*/
    }
}
