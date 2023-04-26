using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverHandler : Singleton<LeverHandler>, ISingleton, IEventObserver
{
    #region ISingleton Variables
    private bool isDone = false;
    public bool IsDoneInitializing
    {
        get { return isDone; }
    }
    #endregion

    #region Event Parameters
    private EventParameters leverParam;
    #endregion
    public void Initialize()
    {

        leverParam = new EventParameters();
        AddEventObservers();

        isDone = true;
    }

    public void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.GAME_START, OnGameStart);
    }

    #region Event Broadcaster Notifications
    public void OnGameStart(EventParameters param = null)
    {
       // _con_belt_refs.ConveyorBelt.MoveBelt();

    }


    #endregion
}
