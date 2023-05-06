using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverData : MonoBehaviour, IResettable
{
    [SerializeField] private int _lever_position;
    public int LeverPosition
    {
        get { return _lever_position; }
        set { _lever_position = value; }
    }

    [SerializeField] private bool _lever_selected;
    public bool LeverSelected
    {
        get { return _lever_selected; }
        set { _lever_selected = value; }
    }

    [SerializeField] private bool _lever_rotating;
    public bool LeverRotating
    {
        get { return _lever_rotating; }
        set { _lever_rotating = value; }
    }


    public void Reset()
    {
        _lever_selected = false;
    }
}
