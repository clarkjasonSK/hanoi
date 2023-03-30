using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingData : MonoBehaviour
{
    [SerializeField] [Range(1,6)] private int _ring_size;
    public int RingSize
    { 
        get { return _ring_size; } 
    }

    [SerializeField] private bool _is_top_ring;
    public bool IsTopRing
    {
        get { return _is_top_ring; }
        set { _is_top_ring = value; }
    }

    [SerializeField] private bool _is_floating;
    public bool IsFloating
    {
        get { return _is_floating; }
        set { _is_top_ring = value; }
    }


    #region StateHandler Variables
    private StateHandler<RingState> _ring_state_handler;
    public StateHandler<RingState> RingStateHandler
    {
        get { return _ring_state_handler; }
    }
    public RingState RingState
    {
        get { return _ring_state_handler.CurrentState; }
    }
    #endregion

}

public enum RingState
{
    STACKED,
    FALLING,
    FLOATING
}
