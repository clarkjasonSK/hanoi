using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SystemBootstrap
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void LoadSystem()
    {
        EventBroadcaster.Instance.Initialize();
        GameManager.Instance.Initialize();

        UIManager.Instance.Initialize();

        if (EventBroadcaster.Instance.IsDoneInitializing &&
            GameManager.Instance.IsDoneInitializing &&
            UIManager.Instance.IsDoneInitializing)
        {
            Debug.Log("System initialized!");
            EventBroadcaster.Instance.PostEvent(EventKeys.MENU_START, null);
        }
    }

}
