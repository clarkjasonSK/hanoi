using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltHandler : Singleton<ConveyorBeltHandler>, ISingleton, IEventObserver
{

    #region ISingleton Variables
    private bool isDone = false;
    public bool IsDoneInitializing
    {
        get { return isDone; }
    }
    #endregion


    [SerializeField] ConveyorBeltRefs _con_belt_refs;

    public void Initialize()
    {
        _con_belt_refs = GetComponent<ConveyorBeltRefs>();
        _con_belt_refs.ConveyorBelt.Initialize();

        AddEventObservers();

        isDone = true;
    }

    public void AddEventObservers()
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
