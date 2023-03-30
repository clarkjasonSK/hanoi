using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>, ISingleton, IEventObserver
{
    #region Singleton Variables
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
        EventBroadcaster.Instance.AddObserver(EventKeys.PLAY_PRESSED, OnPlayPressed);
    }

    public void StartGame()
    {
        EventBroadcaster.Instance.PostEvent(EventKeys.START_GAME, null);
    }

    #region Event Broadcaster Notifications
    public void OnPlayPressed(EventParameters param)
    {
    }

    #endregion

}
