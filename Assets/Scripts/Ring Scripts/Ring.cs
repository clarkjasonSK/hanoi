using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    [SerializeField] private GameValues _game_values;

    #region Ring Variables
    [SerializeField] private RingData _ring_data;
    [SerializeField] private RingController _ring_contrlr;

    public int RingSize
    {
        get { return _ring_data.RingSize; }
    }
    public bool IsSmallestRing
    {
        get { return _ring_data.IsSmallestRing; }
        set { _ring_data.IsSmallestRing = value; }
    }
    #endregion

    #region Ring Audio Variables
    [SerializeField] private SimpleSFX _ring_hit_sfx;

    [SerializeField] private AudioSource _audio_src;
    #endregion

    void Start()
    {
        OnInstantiate();
    }

    public void FloatRing(float floatHeight)
    {
        _ring_contrlr.ResetForces();
        _ring_data.RingStateHandler.SwitchState(RingState.FLOATING);
        _ring_contrlr.StartFloating(floatHeight, _game_values.RingFloatSpeed);

    }

    public void MoveRing(float moveLocation)
    {
        _ring_contrlr.StartMoving(moveLocation, _game_values.RingTravelSpeed);
    }

    public void DropRing(float endLocation)
    {
        _ring_data.RingStateHandler.SwitchState(RingState.STACKED);
        _ring_contrlr.StopMoving(endLocation);
        _ring_contrlr.StopFloating();
    }

    public void OnInstantiate()
    {
        if (_ring_data is null)
        {
            _ring_data = GetComponent<RingData>();
        }
        if (_ring_contrlr is null)
        {
            _ring_contrlr = GetComponent<RingController>();
        }
        if (_audio_src is null)
        {
            _audio_src = GetComponent<AudioSource>();
        }
        if (_game_values is null)
        {
            _game_values = GameManager.Instance.GameValues;
            _ring_contrlr.GameValues = _game_values;
        }

    }

    public void OnActivate()
    {
        gameObject.SetActive(true);
        transform.localPosition += new Vector3(0, (.5f * _ring_data.RingSize), 0);
    }

    public void OnDeactivate()
    {
        transform.localPosition = Vector3.zero;
        _ring_data.Reset();
        _ring_contrlr.Reset();
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        _ring_hit_sfx.PlaySFX(_audio_src);

        if (!_ring_data.IsSmallestRing || collision.gameObject.tag != TagNames.RING)
            return;

        EventBroadcaster.Instance.PostEvent(EventKeys.RING_TOP_STACK, null);
    }
}
