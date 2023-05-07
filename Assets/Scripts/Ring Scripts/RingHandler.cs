using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingHandler : Handler
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

    private int _ring_despawn_count;
    #endregion
    

    #region Ring Handler SFX 
    [SerializeField] private AudioSource _audio_src;
    [SerializeField] private SimpleSFX _ring_float_reset_sfx;
    #endregion

    [SerializeField] private GameValues _game_vals;

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
        _game_vals = GameManager.Instance.GameValues;
        AddEventObservers();
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
        _ring_despawn_count = 0;
    }
   
    public override void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.ASSETS_INIT, OnRingSpawn);
        EventBroadcaster.Instance.AddObserver(EventKeys.ASSETS_RESET, OnAssetsReset);

        EventBroadcaster.Instance.AddObserver(EventKeys.GAME_RESET, FloatToResetRings);
        EventBroadcaster.Instance.AddObserver(EventKeys.RINGS_SPAWN, OnRingSpawn);
        EventBroadcaster.Instance.AddObserver(EventKeys.RING_DESPAWN, OnRingDespawn);

    }

    #endregion

    public void ResetRings()
    {
        foreach (Ring r in _rings)
        {
            _ring_refs.RingLifetime.ReleaseRing(r);
        }
        _rings.Clear();
        _floating_ring = null;
        _ring_despawn_count = 0;
    }
    public void FloatRing(Ring selectedRing, float targetHeight)
    {
        selectedRing.FloatRing(targetHeight);
    }
    public void FloatRing(Ring selectedRing )
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

    private void playResetSFX()
    {
        _ring_float_reset_sfx.PlaySFX(_audio_src);
    }

    private void setRingRefs(EventParameters param)
    {
        ringRef = param.GetParameter<Ring>(EventParamKeys.RING, null);
    }

    #region Event Broadcaster Notifications
    
    public void OnAssetsReset(EventParameters param = null)
    {
        ResetRings();
    }

    public void FloatToResetRings(EventParameters param = null)
    {
        playResetSFX();
        foreach (Ring r in _rings)
        {
            FloatRing(r, _game_vals.RingDespawnHeight);
        }
    }

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
        _ring_despawn_count++;

        if (_ring_despawn_count != GameManager.Instance.RingAmount)
            return;

        EventBroadcaster.Instance.PostEvent(EventKeys.ASSETS_DESPAWNED, null);
    }
    
    #endregion
}
