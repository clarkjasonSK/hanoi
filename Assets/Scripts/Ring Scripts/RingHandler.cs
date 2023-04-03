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


    #region Event Parameters
    private EventParameters _ringh_params;
    #endregion

    #region Initializers
    public void Initialize()
    {
        _rings = new List<Ring>();

        //InstantiateRings(GameManager.Instance.RingAmount);
        _ringh_params = new EventParameters();
        AddEventObservers();

        isDone = true;
    }
    public void InstantiateRings(int ringAmount)
    {
        for (int i = 0; i < ringAmount; i++)
        {
            _rings.Add(_ring_util.RingLifetime.GetNewRing(i));

            _rings[i].IsSmallestRing = i == ringAmount - 1 ? true : false;

            _rings[i].transform.localPosition += _ring_util.RingSpawnHeight.transform.localPosition;

            _ringh_params.AddParameter<Ring>(EventParamKeys.RING,_rings[i]);
            EventBroadcaster.Instance.PostEvent(EventKeys.RING_ADDPOLE, _ringh_params);
        }
    }
    public void ResetRings()
    {
        foreach(Ring r in _rings)
        {
            _ring_util.RingLifetime.ReleaseRing(r);
        }
        _rings.Clear();
        _floating_ring = null;
    }
   
    public void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.RINGS_SPAWN, OnRingSpawn);
        EventBroadcaster.Instance.AddObserver(EventKeys.ASSETS_RESET, OnAssetsReset);
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

    public void OnRingSpawn(EventParameters param = null)
    {
        InstantiateRings(GameManager.Instance.RingAmount);
    }
    public void OnAssetsReset(EventParameters param = null)
    {
        ResetRings();
    }
    #endregion
}
