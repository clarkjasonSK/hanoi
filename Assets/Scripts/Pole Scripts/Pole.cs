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
        get { return _pole_position; }
        set { _pole_position = value; }
    }
    #endregion

    #region Pole Audio Variables
    [SerializeField] private SimpleSFX _pole_hit_sfx;

    [SerializeField] private AudioSource _audio_src;
    #endregion

    [SerializeField] private GameValues _game_values;

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
    public void ResetPole()
    {
        _pole_data.ResetData();
        _pole_contrlr.ResetController();
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(TagNames.RING))
        {
            _pole_hit_sfx.PlaySFX(_audio_src);
        }

        if (collision.gameObject.CompareTag(TagNames.POLE_DESPAWN))
        {
            ///deactivate
        }

    }
}
