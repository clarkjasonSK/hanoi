using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleHandler : Singleton<PoleHandler>, ISingleton, IEventObserver
{
    #region ISingleton Variables
    private bool isDone = false;
    public bool IsDoneInitializing
    {
        get { return isDone; }
    }
    #endregion

    public void Initialize()
    {
       
        AddEventObservers();

        isDone = true;
    }
    public void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.GAME_START, OnGameStart);
        EventBroadcaster.Instance.AddObserver(EventKeys.POLE_PRESS, OnPolePress);
    }


    #region Event Broadcaster Notifications

    public void OnGameStart(EventParameters param = null)
    {

    }
    public void OnPolePress(EventParameters param = null)
    {

    }
    #endregion
}
