using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingHandler : Singleton<RingHandler>, ISingleton, IEventObserver
{
    #region ISingleton Variables
    private bool isDone = false;
    public bool IsDoneInitializing
    {
        get { return isDone; }
    }
    #endregion

    #region Ring Handler Variables
    [SerializeField] private RingUtility _ring_util;
    public RingUtility RingUtility
    {
        set { _ring_util = value; }
    }

    [SerializeField] private List<Ring> _rings;
    [SerializeField] private Ring _floating_ring;
    public bool HasFloatingRing
    {
        get { return _floating_ring is null ? false : true; }
    }
    public int FloatingRingSize
    {
        get { return _floating_ring.RingSize; }
    }
    #endregion

    #region Initializers
    public void Initialize()
    {
        _rings = new List<Ring>();

        InstantiateRings(GameManager.Instance.RingAmount);
        AddEventObservers();

        isDone = true;
    }
    public void InstantiateRings(int ringAmount)
    {
        _rings.Clear();
        for (int i = 0; i < ringAmount; i++)
        {
            _rings.Add(_ring_util.RingLifetime.GetNewRing(i));
            _rings[i].transform.localPosition += _ring_util.RingSpawnHeight.transform.localPosition;
        }
    }
    public void AddEventObservers()
    {
        // EventBroadcaster.Instance.AddObserver(EventKeys.GAME_START, OnGameStart);
    }

    #endregion


    public Ring GetRingAt(int ringIndex)
    {
        return _rings[ringIndex];
    }

    public void FloatRing(Ring selectedRing)
    {
        _floating_ring = selectedRing;
        selectedRing.FloatRing(_ring_util.RingFloatHeight.localPosition.y);
    }

    public void MoveFloatingRing(Vector3 moveLocation)
    {
        _floating_ring.MoveRing(moveLocation.z);
    }

    public void DropRing(Vector3 endLocation)
    {
        _floating_ring.DropRing(endLocation.z);
        _floating_ring = null;
    }


    #region Event Broadcaster Notifications

    public void OnRingSelect(EventParameters param = null)
    {

    }

    #endregion
}
