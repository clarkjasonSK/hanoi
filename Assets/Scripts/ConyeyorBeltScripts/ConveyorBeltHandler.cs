using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltHandler : Handler
{
    [SerializeField] ConveyorBeltRefs _con_belt_refs;

    public override void Initialize()
    {
        _con_belt_refs = GetComponent<ConveyorBeltRefs>();
        _con_belt_refs.ConveyorBelt.Initialize();

        AddEventObservers();
    }

    public override void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.ASSETS_INIT, OnBeltMove);

        EventBroadcaster.Instance.AddObserver(EventKeys.CON_BELT_MOVE, OnBeltMove);
        EventBroadcaster.Instance.AddObserver(EventKeys.CON_BELT_STOP, OnBeltStop);
    }

    #region Event Broadcaster Notifications
    public void OnBeltMove(EventParameters param = null)
    {
        _con_belt_refs.ConveyorBelt.MoveBelt();

    }

    public void OnBeltStop(EventParameters param = null)
    {
        _con_belt_refs.ConveyorBelt.StopBelt();

    }
    #endregion
}
