using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AssetManager", menuName = "ScriptableObjects/Managers/AssetManager")]
public class AssetManager : SingletonSO<AssetManager>, IEventObserver
{
    public override void Initialize()
    {

        AddEventObservers();

        isDone = true;
    }

    public void AddEventObservers()
    {

        EventBroadcaster.Instance.AddObserver(EventKeys.GAME_START, OnGameStart);

        EventBroadcaster.Instance.AddObserver(EventKeys.RING_DROPPED, OnRingDropped);

        EventBroadcaster.Instance.AddObserver(EventKeys.RING_STACKED_FULL, OnRingStackedFull);

        EventBroadcaster.Instance.AddObserver(EventKeys.POLE_END_FULL, OnPoleEndFull);
        EventBroadcaster.Instance.AddObserver(EventKeys.ASSETS_DESPAWNED, OnAssetsDespawned);
        EventBroadcaster.Instance.AddObserver(EventKeys.POLE_MOVE_FINISH, OnPoleMoveFinish);

    }

    #region Event Broadcaster Notifications
    public void OnGameStart(EventParameters param = null)
    {
        EventBroadcaster.Instance.PostEvent(EventKeys.ASSETS_INIT, null);

    }
    public void OnRingDropped(EventParameters param = null)
    {
        GameManager.Instance.IncrMoveCount();
        EventBroadcaster.Instance.PostEvent(EventKeys.COUNT_UPDATE, null);

    }

    public void OnPoleEndFull(EventParameters param = null)
    {
        GameManager.Instance.SetEndPoleFull();
        EventBroadcaster.Instance.PostEvent(EventKeys.ASSETS_DISABLE, null);

    }


    public void OnRingStackedFull(EventParameters param = null)
    {
        if (GameManager.Instance.GoalPoleWhole)
        {
            EventBroadcaster.Instance.PostEvent(EventKeys.PANEL_DROP, null);
        }
    }
    public void OnAssetsDespawned(EventParameters param = null)
    {
        EventBroadcaster.Instance.PostEvent(EventKeys.POLE_ADJUST, null);
        EventBroadcaster.Instance.PostEvent(EventKeys.CON_BELT_MOVE, null);
        EventBroadcaster.Instance.PostEvent(EventKeys.PANEL_RISE, null);

    }

    public void OnPoleMoveFinish(EventParameters param = null)
    {
        EventBroadcaster.Instance.PostEvent(EventKeys.CON_BELT_STOP, null);
        EventBroadcaster.Instance.PostEvent(EventKeys.POLE_SPAWN, null);
        EventBroadcaster.Instance.PostEvent(EventKeys.ASSETS_ENABLE, null);

    }
    #endregion
}
