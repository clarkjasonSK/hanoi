using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{

    #region Ring Variables
    [SerializeField] private RingData _ring_data;
    [SerializeField] private RingController _ring_contrlr;

    public int RingSize
    {
        get { return _ring_data.RingSize; }
    }
    public bool IsSmallestRing
    {
        set { _ring_data.IsSmallestRing = value; }
    }
    #endregion

    #region Ring Audio Variables
    [SerializeField] private AudioSource _audio_src;

    [SerializeField] private SimpleSFX _ring_hit_sfx;
    [SerializeField] private SimpleSFX _ring_float_sfx;
    [SerializeField] private SimpleSFX _ring_drop_sfx;

    #endregion

    #region Event Parameters
    private EventParameters _ring_param;
    #endregion


    [SerializeField] private GameValues _game_values;

    void Start()
    {
        OnInstantiate();
    }

    public void FloatRing(float floatHeight)
    {
        _ring_float_sfx.PlaySFX(_audio_src);
        _ring_data.RingEvent = false;
        _ring_data.RingStateHandler.SwitchState(RingState.FLOATING);
        _ring_contrlr.ResetForces();
        _ring_contrlr.StartFloating(floatHeight, _game_values.RingFloatSpeed);

    }

    public void MoveRing(float moveLocation)
    {
        _ring_contrlr.StartMoving(moveLocation, _game_values.RingTravelSpeed);
    }

    public void DropRing(float endLocation)
    {
        _ring_drop_sfx.PlaySFX(_audio_src);
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
        _ring_param = new EventParameters();
        _ring_param.AddParameter<Ring>(EventParamKeys.RING, this);
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

        if (!_ring_data.RingEvent)
        {
            _ring_data.RingEvent = true;
            EventBroadcaster.Instance.PostEvent(EventKeys.RING_STACKED_FULL, null);
        }

    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag(TagNames.DESPAWN))
        {
            EventBroadcaster.Instance.PostEvent(EventKeys.RING_DESPAWN, _ring_param);
        }
    }

}
