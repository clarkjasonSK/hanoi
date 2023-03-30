using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pole : Poolable
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
    }

    private void OnMouseOver()
    {
        if (_pole_data.IsHovering)
            return;

        EventBroadcaster.Instance.PostEvent(EventKeys.POLE_HOVER, poleParams);
        _pole_data.IsHovering = true;
    }
    void OnMouseDown()
    {
        EventBroadcaster.Instance.PostEvent(EventKeys.POLE_PRESS, poleParams);
    }
    void OnMouseExit()
    {
        _pole_data.IsHovering = false;
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
}
