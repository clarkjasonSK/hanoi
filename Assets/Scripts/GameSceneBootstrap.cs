using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneBootstrap : MonoBehaviour, IBootstrapper
{
    public void Awake()
    {
        LoadSingletonsAndDependencies();
    }
    public void LoadSingletonsAndDependencies()
    {
        GetComponent<PoleRefs>().ObjectPooling.startPooling();

        PoleHandler.Instance.PoleRefs = GetComponent<PoleRefs>();
        PoleHandler.Instance.Initialize();

        RingHandler.Instance.RingRefs = GetComponent<RingRefs>(); ;
        RingHandler.Instance.Initialize();

        PanelHandler.Instance.PanelRefs = GetComponent<PanelRefs>(); ;
        PanelHandler.Instance.Initialize();

        ConveyorBeltHandler.Instance.ConveyorBeltRefs = GetComponent<ConveyorBeltRefs>(); ;
        ConveyorBeltHandler.Instance.Initialize();

        UIManager.Instance.UIRefs = GetComponent<UIRefs>();

        if (RingHandler.Instance.IsDoneInitializing &&
            PoleHandler.Instance.IsDoneInitializing &&
            PanelHandler.Instance.IsDoneInitializing &&
            ConveyorBeltHandler.Instance.IsDoneInitializing
            )
        {
            Debug.Log("Game Scene initialized!");
            EventBroadcaster.Instance.PostEvent(EventKeys.GAME_START, null);
        }
    }
}
