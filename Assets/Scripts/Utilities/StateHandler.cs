using System;
using System.Collections.Generic;
using UnityEngine;

public class StateHandler<T>
{
    //private Dictionary<T, Action> _on_state_enter_default_action;
    //private Dictionary<T, Action> _on_state_exit_default_action;

    private T _current_state;
    public T CurrentState
    {
        get { return _current_state; }
    }

    public void Initialize(T initialState)
    {
        //_current_state = initialState;
        SwitchState(initialState);
        //_on_state_enter_default_action = new Dictionary<T, Action>();
        //_on_state_exit_default_action = new Dictionary<T, Action>();
    }

    public void SwitchState(T nextState/*, Action prevStateExit, Action nextStateEnter*/)
    {
        /*
        if (_on_state_exit_default_action != null && 
            _on_state_exit_default_action.ContainsKey(_current_state)) 
        { 
            _on_state_exit_default_action[_current_state]?.Invoke();
        }
        prevStateExit?.Invoke();*/

        _current_state = nextState;
        Debug.Log("Current state: " + _current_state);
        /*
        if (_on_state_enter_default_action != null &&
            _on_state_enter_default_action.ContainsKey(_current_state))
        {
            _on_state_enter_default_action[_current_state]?.Invoke();
        }
        nextStateEnter?.Invoke();*/
        
    }
    /*
    public void AddStateActionEnter(T state, Action stateEnter)
    {
        if (!_on_state_enter_default_action.ContainsKey(state))
        {
            _on_state_enter_default_action.Add(state, stateEnter);
        }
        else
        {
            _on_state_enter_default_action[state] += stateEnter;
        }
    }
    public void AddStateActionExit(T state, Action stateExit)
    {

        if (!_on_state_exit_default_action.ContainsKey(state))
        {
            _on_state_exit_default_action.Add(state, stateExit);
        }
        else
        {
            _on_state_exit_default_action[state] += stateExit;
        }
    }*/

}
