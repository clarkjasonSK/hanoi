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

    [SerializeField] private GameValues _game_values;

    private IEnumerator _ring_collision_cooldown;

    #region Event Parameters
    private EventParameters _ring_param;
    #endregion

    void Start()
    {
        OnInstantiate();
    }

    public void FloatRing(float floatHeight)
    {
        stopRingCollisionCooldown();

        _ring_float_sfx.PlaySFX(_audio_src);

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
        _ring_param.AddParameter(EventParamKeys.RING, this);


        /*
        var tempShape = _ring_hit_vfx.shape;

        tempShape.radius = Dictionary.RING_VFX_BASE_RAD - (_ring_data.RingSize * Dictionary.RING_VFX_DECREMENT);*/

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
        if (_ring_data.RingCollision)
            return;

        startRingCollisionCooldown();

        _ring_hit_sfx.PlaySFX(_audio_src);

        // attach collision position
        //_ring_param.AddParameter(EventParamKeys.RING_HIT_VFX, new Vector3(transform.position.x, collision.contacts[0].point.y, transform.position.z ) );
        //_ring_param.AddParameter(EventParamKeys.RING_HIT_VFX, transform.position );

        EventBroadcaster.Instance.PostEvent(EventKeys.RING_HIT, _ring_param);

        if (!_ring_data.IsSmallestRing || collision.gameObject.tag != TagNames.RING)
            return;

        EventBroadcaster.Instance.PostEvent(EventKeys.RING_STACKED_FULL, null);

    }
    private void startRingCollisionCooldown()
    {
        if (!this.gameObject.activeInHierarchy)
            return;

        _ring_data.RingCollision = true;
        _ring_collision_cooldown = ringCollisionCooldown();
        StartCoroutine(_ring_collision_cooldown);
    }
    private void stopRingCollisionCooldown()
    {
        if (_ring_collision_cooldown is null)
            return;

        StopCoroutine(_ring_collision_cooldown);
        _ring_data.RingCollision = false;
    }
    private IEnumerator ringCollisionCooldown()
    {
        yield return new WaitForSeconds(_game_values.RingCollisionCooldown);

        _ring_data.RingCollision = false;

        yield break;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag(TagNames.DESPAWN))
        {
            EventBroadcaster.Instance.PostEvent(EventKeys.RING_DESPAWN, _ring_param);
        }
    }

}
