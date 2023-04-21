using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltHandler : Singleton<ConveyorBeltHandler>, IEventObserver
{

    [SerializeField] ConveyorBeltRefs _con_belt_refs;

    public override void Initialize()
    {
        _con_belt_refs = GetComponent<ConveyorBeltRefs>();
        //Debug.Log("conbelt GO: " + this.gameObject.name);
        //Debug.Log("conbelt refs: " + _con_belt_refs );
        _con_belt_refs.ConveyorBelt.Initialize();

        AddEventObservers();

        Debug.Log(" Conveyor belt handler initialized! " + gameObject.name);
        isDone = true;
    }

    public void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.GAME_START, OnBeltMove);
        EventBroadcaster.Instance.AddObserver(EventKeys.DESPAWN_DONE, OnBeltMove);

        EventBroadcaster.Instance.AddObserver(EventKeys.POLE_MOVE_FINISH, OnBeltStop);
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
