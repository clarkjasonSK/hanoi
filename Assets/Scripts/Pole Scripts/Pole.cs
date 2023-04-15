using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pole : Poolable
{
    #region Pole Variables
    [SerializeField] private PoleController _pole_contrlr;
    [SerializeField] private PoleData _pole_data;
    public bool IsHoveringOver
    {
        get { return _pole_data.IsHovering; }
        set { _pole_data.IsHovering = value; }
    }
    [SerializeField] private PolePosition _pole_position;
    public PolePosition PolePosition
    {
        set { _pole_position = value; }
    }
    #endregion

    #region Pole Audio Variables
    [SerializeField] private SimpleSFX _pole_hit_sfx;

    [SerializeField] private AudioSource _audio_src;
    #endregion

    [SerializeField] private GameValues _game_values;

    #region Event Parameters
    private EventParameters _pole_param;
    #endregion

    public void AddRingToPole(Ring ring)
    {
        _pole_data.AddRing(ring);
    }

    public Ring BorrowTopRing()
    {
        return _pole_data.TopRing;
    }
    public Ring RemoveTopRing()
    {
        return _pole_data.RemoveTopRing();
    }
    public int GetRingCount()
    {
        return _pole_data.StackCount;
    }
    public void PoleMoveFinish()
    {
        if(_pole_position.PoleOrder == PoleDictionary.END_POS)
        {
            EventBroadcaster.Instance.PostEvent(EventKeys.POLE_MOVE_FINISH, null);
        }
    }
    public void ResetPole()
    {
        _pole_data.ResetData();
        _pole_contrlr.ResetController();
    }
    public int GetPolePosition()
    {
        return _pole_position.PoleOrder;
    }
    public void MoveToPolePosition()
    {
        _pole_contrlr.MoveToLocation(_pole_position.GetLocation(), _game_values.PoleMoveSpeed);
    }

    #region Poolable Methods
    public override void OnInstantiate()
    {
        if (_pole_data is null)
        {
            _pole_data = GetComponent<PoleData>();
        }
        if (_pole_contrlr is null)
        {
            _pole_contrlr = GetComponent<PoleController>();
        }
        if (_audio_src is null)
        {
            _audio_src = GetComponent<AudioSource>();
        }

        if (_game_values is null)
        {
            _game_values = GameManager.Instance.GameValues;
        }

        _pole_param = new EventParameters();
        _pole_param.AddParameter<Pole>(EventParamKeys.POLE, this);
    }

    public override void OnActivate()
    {

    }

    public override void OnDeactivate()
    {
        _pole_data.ResetData();
        _pole_contrlr.ResetController();
    }
    #endregion

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag(TagNames.RING))
        {
            _pole_hit_sfx.PlaySFX(_audio_src);
        }

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag(TagNames.DESPAWN))
        {
            EventBroadcaster.Instance.PostEvent(EventKeys.POLE_DESPAWN, _pole_param);
            //this.gameObject.SetActive(false); // TEMPORARY
        }

    }
}
