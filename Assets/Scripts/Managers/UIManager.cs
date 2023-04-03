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

    [SerializeField] private UIRefs _ui_refs;
    public UIRefs UIRefs
    {
        set { _ui_refs = value; }
    }


    public void Initialize()
    {
        AddEventObservers();
        
        isDone = true;
    }

    public void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.COUNT_UPDATE, OnCountUpdate);
        EventBroadcaster.Instance.AddObserver(EventKeys.SLIDER_CHANGE, OnSliderChange);
    }


    #region Event Broadcaster Notifications
    public void OnCountUpdate(EventParameters param)
    {
        _ui_refs.GameUI.IncrCount(param.GetParameter<int>(EventParamKeys.MOVE_COUNT, 0));

    }
    public void OnSliderChange(EventParameters param)
    {

    }

    #endregion

}
