using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverData : MonoBehaviour
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


    public void ResetData(int defaultPos)
    {
        _lever_position = defaultPos;
        _lever_selected = false;
    }
}
