using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBootup : MonoBehaviour, IBootstrapper
{
    public ObjectPooling ObjectPool;
    public PoleUtility PoleUtility;
    public RingUtility RingUtility;

    public void Awake()
    {
        LoadSingletonsAndDependencies();
    }
    public void LoadSingletonsAndDependencies()
    {
        ObjectPool.startPooling();
        RingHandler.Instance.RingUtility = RingUtility;
        RingHandler.Instance.Initialize();

        PoleHandler.Instance.PoleUtility = PoleUtility;
        PoleHandler.Instance.Initialize();


        if (RingHandler.Instance.IsDoneInitializing &&
            PoleHandler.Instance.IsDoneInitializing)
        {
            Debug.Log("Game Scene initialized!");
            EventBroadcaster.Instance.PostEvent(EventKeys.GAME_START, null);
        }
    }
}
