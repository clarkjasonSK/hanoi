using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIHandler : Singleton<GameUIHandler>, IInitializable, IEventObserver
{

    [SerializeField] private GameUIRefs _game_ui_refs;

    #region Cache Refs
    private int tempAmnt;
    #endregion

    public override void Initialize()
    {
        _game_ui_refs = GetComponent<GameUIRefs>();

        AddEventObservers();
    }

    public override void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.ASSETS_RESET, OnAssetReset);
        EventBroadcaster.Instance.AddObserver(EventKeys.ASSETS_DISABLE, onAssetsDisable);
        EventBroadcaster.Instance.AddObserver(EventKeys.ASSETS_ENABLE, onAssetsEnable);

        EventBroadcaster.Instance.AddObserver(EventKeys.COUNT_UPDATE, OnCountUpdate);
        EventBroadcaster.Instance.AddObserver(EventKeys.SLIDER_CHANGE, OnSliderChange);
    }

    public void ResetButtonClicked()
    {
        EventBroadcaster.Instance.PostEvent(EventKeys.GAME_RESET, null);
    }


    #region Event Broadcaster Notifications
    public void OnCountUpdate(EventParameters param)
    {
        _game_ui_refs.GameUI.IncrCount(GameManager.Instance.MoveCount);

    }
    public void OnSliderChange(EventParameters param)
    {
        tempAmnt = param.GetParameter<int>(EventParamKeys.SLIDER_NUMBER, 0);

        if (tempAmnt == GameManager.Instance.RingAmount)
            return;

        _game_ui_refs.GameUI.SetRingCount(tempAmnt);
        GameManager.Instance.SetRingsAmount(tempAmnt);
        EventBroadcaster.Instance.PostEvent(EventKeys.GAME_RESET, null);
    }
    public void OnAssetReset(EventParameters param = null)
    {
        _game_ui_refs.GameUI.ResetCount();
    }

    public void onAssetsDisable(EventParameters param = null)
    {
        _game_ui_refs.GameUI.ToggleButton(false);
        _game_ui_refs.SliderScript.ToggleSlider(false);
    }
    public void onAssetsEnable(EventParameters param = null)
    {
        _game_ui_refs.GameUI.ToggleButton(true);
        _game_ui_refs.SliderScript.ToggleSlider(true);
    }

    #endregion

}
