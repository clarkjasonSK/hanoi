using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorHandler : Handler
{
    [SerializeField] private MonitorRefs _monitor_refs;

    [SerializeField] private VisualValues _vis_vals;

    public override void Initialize()
    {
        if (_monitor_refs is null)
            _monitor_refs = GetComponent<MonitorRefs>();

        if (_vis_vals is null)
            _vis_vals = AssetManager.Instance.VisualValues;

        AddEventObservers();
    }
    public override void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.ASSETS_INIT, OnAssetReset);
        EventBroadcaster.Instance.AddObserver(EventKeys.ASSETS_RESET, OnAssetReset);
        EventBroadcaster.Instance.AddObserver(EventKeys.COUNT_UPDATE, OnCountUpdate);

    }


    #region Event Broadcaster Notifications
    public void OnAssetReset(EventParameters param = null)
    {
        _monitor_refs.Monitor.Reset();
        _monitor_refs.Monitor.LeastPossibleSet(GameManager.Instance.RingAmount);
    }
    public void OnCountUpdate(EventParameters param = null)
    {
        _monitor_refs.Monitor.MoveCountSet(GameManager.Instance.MoveCount);
    }

    #endregion
}
