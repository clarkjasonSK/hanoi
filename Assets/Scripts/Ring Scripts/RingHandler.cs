using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingHandler : Singleton<RingHandler>, ISingleton, IEventObserver
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
        // EventBroadcaster.Instance.AddObserver(EventKeys.GAME_START, OnGameStart);
    }


    #region Event Broadcaster Notifications

    public void OnGameStart(EventParameters param = null)
    {

    }

    #endregion
}
