using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Lever : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private LeverData _lever_data;
    [SerializeField] private LeverController _lever_ctrler;

    [SerializeField] private Transform _lever_handle;

    [SerializeField] private GameValues _game_values;

    public bool LeverSelected
    {
        get { return _lever_data.LeverSelected; }
    }
    public bool LeverRotating
    {
        get { return _lever_data.LeverRotating; }
    }
    public int LeverPos
    {
        get { return _lever_data.LeverPosition; }
    }

    #region Event Parameters
    private EventParameters leverParam;
    #endregion
    private void Start()
    {
        if (_lever_data is null)
            _lever_data = GetComponent<LeverData>();
        if (_lever_ctrler is null)
            _lever_ctrler = GetComponent<LeverController>();
        if (_game_values is null)
            _game_values = GameManager.Instance.GameValues;

        _lever_ctrler.Initialize(this, _lever_handle);
        leverParam = new EventParameters();
        leverParam.AddParameter(EventParamKeys.LEVER, this);
    }

    public void RotateLever(int targetPos, int targetRot)
    {
        _lever_data.LeverPosition = targetPos;
        _lever_data.LeverRotating = true;
        _lever_ctrler.StartRotating(targetRot, _game_values.LeverRotateSpeed);
    }
    public void RotateFinish()
    {
        _lever_data.LeverRotating = false;
        _lever_ctrler.StopRotating();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        _lever_data.LeverSelected = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _lever_data.LeverSelected = false;

        EventBroadcaster.Instance.PostEvent(EventKeys.LEVER_POS_CHOSEN, leverParam);
        // lever is deselected
        // post event asset restart with selected lever position
    }

    public void ToggleHandle(bool toggle)
    {
        _lever_handle.gameObject.GetComponent<BoxCollider>().enabled = toggle;
    }
}
