using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelHandler : Singleton<PanelHandler>, ISingleton, IEventObserver
{
    #region ISingleton Variables
    private bool isDone = false;
    public bool IsDoneInitializing
    {
        get { return isDone; }
    }
    #endregion

    [SerializeField] private PanelRefs _panel_refs;
    public PanelRefs PanelRefs
    {
        set { _panel_refs = value; }
    }

    public void Initialize()
    {

        AddEventObservers();

        isDone = true;
    }
    public void AddEventObservers()
    {

        EventBroadcaster.Instance.AddObserver(EventKeys.PANEL_DROP, OnPanelDrop);
        EventBroadcaster.Instance.AddObserver(EventKeys.PANEL_RISE, OnPanelRise);
        EventBroadcaster.Instance.AddObserver(EventKeys.DESPAWN_DONE, OnPanelRise); // same a panel rise, just in case panel rise would need to be called by itself
    }


    #region Event Broadcaster Notifications

    public void OnPanelDrop(EventParameters param = null)
    {
        Debug.Log("panel drop!");
        _panel_refs.Panel.MovePanel(_panel_refs._panel_low_pos.transform);
    }
    public void OnPanelRise(EventParameters param = null)
    {
        _panel_refs.Panel.MovePanel(_panel_refs._panel_top_pos.transform);
    }

    #endregion
}

