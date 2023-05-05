using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorData : MonoBehaviour, IResettable
{
    [SerializeField] private int _move_count;
    public int MoveCount
    {
        get { return _move_count; }
        set { _move_count = value; }
    }

    [SerializeField] private int _move_least_possible;
    public int MoveLeastPossible
    {
        get { return _move_least_possible; }
        set { _move_least_possible = value; }
    }

    public void MoveIncrement()
    {
        _move_count++;
    }

    public void Reset()
    {
        _move_count = 0;
        _move_least_possible = 1;
    }

}
