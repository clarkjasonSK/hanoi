using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : Handler
{
    [SerializeField] ButtonRefs _button_refs;

    [SerializeField] VisualValues _vis_vals;

    public override void Initialize()
    {
        if (_button_refs is null)
            _button_refs = GetComponent<ButtonRefs>();

        if (_vis_vals is null)
            _vis_vals = AssetManager.Instance.VisualValues;

        AddEventObservers();
    }

    public override void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.BUTTON_RESET_CLICKED, OnButtonResetClicked);
        EventBroadcaster.Instance.AddObserver(EventKeys.ASSETS_DISABLE, onAssetsDisable);
        EventBroadcaster.Instance.AddObserver(EventKeys.ASSETS_ENABLE, onAssetsEnable);
    }


    #region Event Broadcaster Notifications

    public void OnButtonResetClicked(EventParameters param = null)
    {
        _button_refs.ButtonScript.ButtonPress(_vis_vals.ButtonDefaultPos, _vis_vals.ButtonPressedPos, _vis_vals.ButtonReleaseSpeed);
        EventBroadcaster.Instance.PostEvent(EventKeys.GAME_RESET, null);

    }

    public void onAssetsDisable(EventParameters param = null)
    {
        _button_refs.ButtonScript.ToggleButton(false);
    }
    public void onAssetsEnable(EventParameters param = null)
    {
        _button_refs.ButtonScript.ToggleButton(true);
    }
    #endregion
}
