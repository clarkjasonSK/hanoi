using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingHandler : Singleton<RingHandler>, IEventObserver
{
    #region Ring Handler Variables
    [SerializeField] private RingRefs _ring_refs;

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
    private EventParameters _ring_params;
    #endregion

    #region Cache Refs
    private Ring ringRef;
    //private bool despawnedRingSize;
    #endregion

    #region Initializers
    public override void Initialize()
    {
        _ring_refs = GetComponent<RingRefs>();
        _rings = new List<Ring>();
        _ring_params = new EventParameters();
        AddEventObservers();

        isDone = true;
    }
    public void InstantiateRings(int ringAmount)
    {
        for (int i = 0; i < ringAmount; i++)
        {
            _rings.Add(_ring_refs.RingLifetime.GetNewRing(i));

            _rings[i].IsSmallestRing = i == ringAmount - 1 ? true : false;

            _rings[i].transform.localPosition += _ring_refs.RingSpawnPos.transform.localPosition;

            _ring_params.AddParameter<Ring>(EventParamKeys.RING,_rings[i]);
            EventBroadcaster.Instance.PostEvent(EventKeys.POLE_ADD_RING, _ring_params);
        }
    }
    public void ResetRings()
    {
        foreach(Ring r in _rings)
        {
            _ring_refs.RingLifetime.ReleaseRing(r);
        }
        _rings.Clear();
        _floating_ring = null;
    }
   
    public void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.GAME_START, OnRingSpawn);

        EventBroadcaster.Instance.AddObserver(EventKeys.RINGS_SPAWN, OnRingSpawn);
        EventBroadcaster.Instance.AddObserver(EventKeys.RINGS_DESPAWN, OnRingDespawn);
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
        selectedRing.FloatRing(_ring_refs.RingFloatHeight.localPosition.y);
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

    private void setRingRefs(EventParameters param)
    {
        ringRef = param.GetParameter<Ring>(EventParamKeys.RING, null);
    }

    #region Event Broadcaster Notifications
    /*
    public void OnPosEndEnter(EventParameters param)
    {
        OnRingSpawn(param);
    }*/
    public void OnRingSpawn(EventParameters param = null)
    {
        InstantiateRings(GameManager.Instance.RingAmount);
    }
    public void OnRingDespawn(EventParameters param)
    {
        setRingRefs(param);
        //despawnedRingSize = ringRef.RingSize;
        _rings.Remove(ringRef);
        _ring_refs.RingLifetime.ReleaseRing(ringRef);

        if (!param.GetParameter<bool>(EventParamKeys.RING_IS_SMALLEST, false))
            return;

        EventBroadcaster.Instance.PostEvent(EventKeys.DESPAWN_DONE, null);
    }
    

    public void OnAssetsReset(EventParameters param = null)
    {
        ResetRings();
    }
    #endregion
}
