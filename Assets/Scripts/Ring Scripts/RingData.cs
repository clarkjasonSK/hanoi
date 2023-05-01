using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingData : MonoBehaviour
{
    [SerializeField] [Range(1,10)] private int _ring_size;
    public int RingSize
    { 
        get { return _ring_size; } 
    }

    [SerializeField] private bool _is_smallest_ring;
    public bool IsSmallestRing
    {
        get { return _is_smallest_ring; }
        set { _is_smallest_ring = value; }
    }

    private bool _ring_collision;
    public bool RingCollision
    {
        get { return _ring_collision; }
        set { _ring_collision = value; }
    }

    #region StateHandler Variables
    private StateHandler<RingState> _ring_state_handler = new StateHandler<RingState>();
    public StateHandler<RingState> RingStateHandler
    {
        get { return _ring_state_handler; }
    }
    public RingState RingState
    {
        get { return _ring_state_handler.CurrentState; }
    }
    #endregion

    public void Reset()
    {
        //_ring_size = 10 - _ring_size;
        _is_smallest_ring = false;
        _ring_collision = false;
        _ring_state_handler.SwitchState(RingState.STACKED);
    }


}

public enum RingState
{
    STACKED,
    FLOATING
}
