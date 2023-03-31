using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : Poolable
{
    [SerializeField] private GameValues _game_values;

    #region Ring Variables
    [SerializeField] private RingData _ring_data;
    [SerializeField] private RingController _ring_contrlr;
    public int RingSize
    {
        get { return _ring_data.RingSize; }
    }

    #endregion

    void Start()
    {
        _ring_data.Reset();
    }

    public void FloatRing(float floatHeight)
    {
        _ring_contrlr.Reset();
        _ring_data.RingStateHandler.SwitchState(RingState.FLOATING);
        _ring_contrlr.StartFloating(floatHeight, _game_values.RingFloatSpeed);

    }

    public void MoveRing(float moveLocation)
    {
        _ring_contrlr.StartMoving(moveLocation, _game_values.RingTravelSpeed);
    }

    public void DropRing(float endLocation)
    {
        _ring_data.RingStateHandler.SwitchState(RingState.STACKED);
        _ring_contrlr.StopMoving(endLocation);
        _ring_contrlr.StopFloating();
    }

    public override void OnInstantiate()
    {
        if (_ring_data is null)
        {
            _ring_data = GetComponent<RingData>();
        }
        if (_ring_contrlr is null)
        {
            _ring_contrlr = GetComponent<RingController>();
        }

        if (_game_values is null)
        {
            _game_values = GameManager.Instance.GameValues;
        }
    }

    public override void OnActivate()
    {
        transform.localPosition += new Vector3(0, (.5f * _ring_data.RingSize), 0);
    }

    public override void OnDeactivate()
    {
        _ring_data.Reset();
    }
}
