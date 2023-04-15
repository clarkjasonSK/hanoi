using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] ConveyorBeltController _con_belt_ctrller;

    [SerializeField] GameValues _game_values;

    public void Initialize()
    {
        if (_con_belt_ctrller is null)
            _con_belt_ctrller = GetComponent<ConveyorBeltController>();

        if (_game_values is null)
            _game_values = GameManager.Instance.GameValues;
    }

    public void MoveBelt()
    {
        _con_belt_ctrller.StartMoving(_game_values.BeltMoveSpeed);
    }
    public void StopBelt()
    {
        _con_belt_ctrller.StopMoving();
    }
}
