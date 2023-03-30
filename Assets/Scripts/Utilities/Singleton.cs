using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 Inherit from this base class to create a singleton.
 e.g. public class MyClassName : Singleton<MyClassName>{}
*/
public abstract class Singleton<T> : Singleton where T : MonoBehaviour //abstract singleton with generic T of constraint type monobehavior
{
    private static readonly object _lock = new object(); // lock for multithreading safety
    [SerializeField] private bool _persistent = true; // flag for the singleton to be persistent across scenes

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
            lock (_lock)
            {
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
                var singletonObject = new GameObject();
                singletonObject.name = typeof(T).ToString() + " (Singleton)";

                return _instance = singletonObject.AddComponent<T>(); // possible condition 4; creates and attaches to new gameobject and returns
            }
            
        }
    }

    private void Awake()
    {
        if (_persistent)
        {
            DontDestroyOnLoad(gameObject); // where gameObject is the current gameobject the instance is attached to
        }
    }

    protected virtual void OnAwake() {} // optional to use to use an an Init method; virtual keyword due to the optionality
}

public abstract class Singleton : MonoBehaviour
{
    public static bool _shutting_down { get; private set; }

    private void OnApplicationQuit()
    {
        _shutting_down = true;
    }

    private void OnDestroy()
    {
        _shutting_down = true;
    }
}
