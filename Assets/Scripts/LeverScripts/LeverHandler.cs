using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverHandler : Handler, IEventObserver
{
    [SerializeField] private LeverRefs _lever_refs;

    #region Cache Params
    private Lever leverKey;
    private LeverPosition leverPosKey;
    #endregion

    public override void Initialize()
    {
        if (_lever_refs is null)
            _lever_refs = GetComponent<LeverRefs>();

        AddEventObservers();
    }

    public void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.LEVER_POS_HOVER, OnLeverPosHover);
        EventBroadcaster.Instance.AddObserver(EventKeys.LEVER_POS_CHOSEN, OnLeverPosChosen);

        EventBroadcaster.Instance.AddObserver(EventKeys.ASSETS_DISABLE, onAssetsDisable);
        EventBroadcaster.Instance.AddObserver(EventKeys.ASSETS_ENABLE, onAssetsEnable);
    }

    private void setChosenLeverNumber(int leverNumber)
    {
        //sets the text mesh to match the number and angle of given levernumber.
        // do this instead of assigning new color every time
        _lever_refs.LeverChosenNumber.text = "" + leverNumber;
        _lever_refs.LeverChosenNumber.transform.localEulerAngles = new Vector3(0,0, LeverHelper.ConvertEulerAngle(72-(36*(leverNumber-3))));
    }
    private void setLeverKey(EventParameters param)
    {
        leverKey = param.GetParameter<Lever>(EventParamKeys.LEVER, null);
    }
    private void setPosKey(EventParameters param)
    {
        leverPosKey = param.GetParameter<LeverPosition>(EventParamKeys.LEVER_POS, null);
    }

    #region Event Broadcaster Notifications

    public void OnLeverPosHover(EventParameters param)
    {
        if (!_lever_refs.Lever.LeverSelected)
            return;

        setPosKey(param);
        if (_lever_refs.Lever.LeverPos == leverPosKey.LeverPosNum)
            return;

        _lever_refs.Lever.RotateLever(leverPosKey.LeverPosNum, leverPosKey.LeverPosRotationX);
    }
    public void OnLeverPosChosen(EventParameters param)
    {
        setLeverKey(param);

        if (leverKey.LeverPos == GameManager.Instance.RingAmount)
            return;

        setChosenLeverNumber(leverKey.LeverPos);
        GameManager.Instance.SetRingsAmount(leverKey.LeverPos);
        EventBroadcaster.Instance.PostEvent(EventKeys.GAME_RESET, null);
    }

    public void onAssetsDisable(EventParameters param = null)
    {
        _lever_refs.Lever.ToggleHandle(false);
    }
    public void onAssetsEnable(EventParameters param = null)
    {
        _lever_refs.Lever.ToggleHandle(true);
    }

    #endregion

}
