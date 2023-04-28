using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class SystemBootstrap
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void LoadSystem()
    {
        //Debug.Log("editorprefs autosave: ! " + EditorPrefs.GetInt("kAutoRefresh"));
        //EditorPrefs.SetInt("kAutoRefresh", 1);
        //Debug.Log("editorprefs autosave: " + EditorPrefs.GetInt("kAutoRefresh"));

        EventBroadcaster.Instance.Initialize();
        GameManager.Instance.Initialize();
        AssetManager.Instance.Initialize();

        UIManager.Instance.Initialize();

        if (EventBroadcaster.Instance.IsDoneInitializing &&
            GameManager.Instance.IsDoneInitializing &&
            AssetManager.Instance.IsDoneInitializing &&
            UIManager.Instance.IsDoneInitializing)
        {
            Debug.Log("System initialized!");
            EventBroadcaster.Instance.PostEvent(EventKeys.SYSTEM_START, null);
        }
    }

}
