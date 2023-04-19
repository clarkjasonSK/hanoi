using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 Inherit from this base class to create a singleton.
 e.g. public class MyClassName : SingletonSO<MyClassName>{}
*/
public abstract class SingletonSO<T> : SingletonSO where T : ScriptableObject //abstract singleton with generic T of constraint type ScriptableObject
{

    private static T _instance; // local instance reference
    public static T Instance
    {
        get
        {
            if (_shutting_down)
            {
                Debug.LogWarning("Error 404: Singleton  '" + typeof(T) +
                    "' is shutting down.");
                return null;
            }

            if (_instance != null)
                return _instance; // possible condition 1; instantly returns
            var instances = FindObjectsOfType<T>();
            var count = instances.Length;

            if (count > 0)
            {
                if (count == 1)
                    return _instance = instances[0];// possible condition 2; returns only instance found
                for (int i = 1; i < count; i++)
                    Destroy(instances[i]);
                return instances[0];// possible condition 3; returns first instance found after destroying others
            }
            //Debug.Log("else:");
            return _instance = ScriptableObjectsHelper.GetSO<T>(FileNames.SO_MANAGERS + typeof(T).ToString());
        }
    }

}

public abstract class SingletonSO : ScriptableObject
{
    public static bool _shutting_down { get; private set; }

    private void OnDestroy()
    {
        _shutting_down = true;
    }
}
