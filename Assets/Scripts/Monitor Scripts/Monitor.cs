using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monitor : MonoBehaviour, IResettable
{
    [SerializeField] private MonitorData _monitor_data;
    [SerializeField] private MonitorController _monitor_ctrler;

    public int MoveCount
    {
        get { return _monitor_data.MoveCount; }
    }

    private void Start()
    {
        if (_monitor_data is null)
            _monitor_data = GetComponent<MonitorData>();
        if (_monitor_ctrler is null)
            _monitor_ctrler = GetComponent<MonitorController>();

        Reset();
    }

    public void Reset()
    {
        _monitor_data.Reset();
        _monitor_ctrler.Reset();
    }
    public void MoveCountSet(int moveCount)
    {
        _monitor_data.MoveCount = moveCount;
        _monitor_ctrler.CountSet(_monitor_data.MoveCount);

        if (_monitor_data.MoveCount < _monitor_data.MoveLeastPossible)
        {
            _monitor_ctrler.ColorSet(Dictionary.MOVES_UNDER);
            return;
        }

        if(_monitor_data.MoveCount > _monitor_data.MoveLeastPossible)
        {
            _monitor_ctrler.ColorSet(Dictionary.MOVES_OVER);
            return;
        }
        
        _monitor_ctrler.ColorSet(Dictionary.MOVES_SUCESS);
    }

    public void LeastPossibleSet(int ringAmount)
    {
        for(int i=1; i<= ringAmount; i++)
        {
            _monitor_data.MoveLeastPossible *= 2;
        }

        _monitor_data.MoveLeastPossible--;
        _monitor_ctrler.LeastSet(_monitor_data.MoveLeastPossible);
    }
}
