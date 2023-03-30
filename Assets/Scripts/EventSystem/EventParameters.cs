using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventParameters
{
    public Dictionary<string, object> _event_parameters;

    public EventParameters()
    {
        _event_parameters = new Dictionary<string, object>();
    }

    //adds or replaces at "key" with object parameter "value" of generic type "T" 
    public void AddParameter<T>(string key, T value)
    {
        if (this._event_parameters.ContainsKey(key))
            this._event_parameters[key] = value;
        else
            this._event_parameters.Add(key, value);
    }

    //returns object parameter "value" of generic type "T" at "key" if not null 
    public T GetParameter<T>(string key, T defaultValue)
    {
        if (this._event_parameters.ContainsKey(key))
            return (T)this._event_parameters[key];
        else
            return defaultValue;
    }
}
