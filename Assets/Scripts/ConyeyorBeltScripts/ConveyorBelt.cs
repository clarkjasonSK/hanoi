using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] ConveyorBeltController _con_belt_ctrller;

    #region Ring Audio Variables
    [SerializeField] private AudioSource _audio_src;

    [SerializeField] private SimpleSFX _con_belt_move;
    [SerializeField] private SimpleSFX _con_belt_stop;
    #endregion

    [SerializeField] GameValues _game_values;
    public void Initialize()
    {
        _con_belt_ctrller = GetComponent<ConveyorBeltController>();
        _con_belt_ctrller.ResetBeltMat();
        _audio_src = GetComponent<AudioSource>();

        if (_game_values is null)
            _game_values = GameManager.Instance.GameValues;
    }

    public void MoveBelt()
    {
        _con_belt_move.PlaySFX(_audio_src);
        _con_belt_ctrller.StartMoving(_game_values.BeltMoveSpeed);
    }
    public void StopBelt()
    {
        //_con_belt_move.StopSFX(_audio_src, false);
        _audio_src.Stop();
        _con_belt_stop.PlaySFX(_audio_src);
        _con_belt_ctrller.StopMoving();
    }
}
