using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : Poolable
{
    #region Ring Variables
    [SerializeField] private RingData _ring_data;
    [SerializeField] private RingController _ring_contrlr;
    public int RingSize
    {
        get { return _ring_data.RingSize; }
    }

    #endregion

    [SerializeField] private GameValues _game_values;


    private void Start()
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
