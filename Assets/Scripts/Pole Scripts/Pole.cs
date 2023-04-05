using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pole : Poolable, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    #region Pole Variables
    [SerializeField] private PoleData _pole_data;
    [SerializeField] private PoleController _pole_contrlr;
    #endregion

    [SerializeField] private GameValues _game_values;

    #region Event Parameters
    private EventParameters poleParams;
    #endregion
    void Start()
    {
        if (_pole_data is null)
        {
            _pole_data = GetComponent<PoleData>();
        }
        if (_pole_contrlr is null)
        {
            _pole_contrlr = GetComponent<PoleController>();
        }

        if (_game_values is null)
        {
            _game_values = GameManager.Instance.GameValues;
        }
        poleParams = new EventParameters();
        poleParams.AddParameter(EventParamKeys.SELECTED_POLE, this);

        if(_pole_data.PolePosition == 1)
        {
            //_pole_data.AddRing(RingHandler.Get);
        }
    }

    public void AddRingToPole(Ring ring)
    {
        _pole_data.AddRing(ring);
    }

    public Ring BorrowTopRing()
    {
        return _pole_data.TopRing;
    }
    public Ring RemoveTopRing()
    {
        return _pole_data.RemoveTopRing();
    }
    public int GetRingCount()
    {
        return _pole_data.StackCount;
    }
    public void DepletePole()
    {
        _pole_data.DepleteStack();
    }

    private void OnMouseEnter()
    {
    }
    void OnMouseDown()
    {
    }
    void OnMouseExit()
    {
    }

    public override void OnInstantiate()
    {
        throw new System.NotImplementedException();
    }

    public override void OnActivate()
    {
        throw new System.NotImplementedException();
    }

    public override void OnDeactivate()
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_pole_data.IsHovering)
            return;

        EventBroadcaster.Instance.PostEvent(EventKeys.POLE_HOVER, poleParams);
        _pole_data.IsHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _pole_data.IsHovering = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        EventBroadcaster.Instance.PostEvent(EventKeys.POLE_PRESS, poleParams);
    }
}
