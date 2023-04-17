using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelData : MonoBehaviour
{
    StateHandler<PanelState> _panel_state;

    public PanelState PanelState
    {
        get { return _panel_state.CurrentState; }
    }

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        _panel_state = new StateHandler<PanelState>();
        _panel_state.Initialize(PanelState.RAISED);
    }

    public void TogglePanelState()
    {
        switch (_panel_state.CurrentState)
        {
            case PanelState.RAISED:
                _panel_state.SwitchState(PanelState.LOWERED);
                break;
            case PanelState.LOWERED:
                _panel_state.SwitchState(PanelState.RAISED);
                break;

        }
    }
}


public enum PanelState
{
    RAISED,
    LOWERED
}
