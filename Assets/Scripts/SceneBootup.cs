using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBootup : MonoBehaviour, IBootstrapper
{
    public void Awake()
    {
        LoadSingletonsAndDependencies();
    }
    public void LoadSingletonsAndDependencies()
    {
        RingHandler.Instance.Initialize();
        PoleHandler.Instance.Initialize();


        if (RingHandler.Instance.IsDoneInitializing &&
            PoleHandler.Instance.IsDoneInitializing)
        {
            Debug.Log("Game Scene initialized!");
            EventBroadcaster.Instance.PostEvent(EventKeys.GAME_START, null);
        }
    }
}
