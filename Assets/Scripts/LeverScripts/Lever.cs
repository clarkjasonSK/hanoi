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


    private void Start()
    {
        if (_lever_data is null)
            _lever_data = GetComponent<LeverData>();
        if (_lever_ctrler is null)
            _lever_ctrler = GetComponent<LeverController>();

        _lever_ctrler.LeverHandle = _lever_handle;
    }

    public void RotateLever(float targetRot)
    {
        _lever_ctrler.StartRotating(targetRot, _game_values.LeverRotateSpeed);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // lever is selected in LeverData
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // lever is deselected
        // post event asset restart with selected lever position
    }
}
