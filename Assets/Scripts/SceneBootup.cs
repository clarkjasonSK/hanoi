using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBootup : MonoBehaviour, IBootstrapper
{
    public ObjectPooling ObjectPool;
    public PoleUtility PoleUtility;
    public RingUtility RingUtility;
    public UIRefs UIRefs;
    public PanelRefs PanelRefs;
    public ConveyorBeltRefs ConveyorBeltRefs; 

    public void Awake()
    {
        LoadSingletonsAndDependencies();
    }
    public void LoadSingletonsAndDependencies()
    {
        ObjectPool.startPooling();
        PoleHandler.Instance.PoleUtility = PoleUtility;
        PoleHandler.Instance.Initialize();

        RingHandler.Instance.RingUtility = RingUtility;
        RingHandler.Instance.Initialize();

        PanelHandler.Instance.PanelRefs = PanelRefs;
        PanelHandler.Instance.Initialize();

        ConveyorBeltHandler.Instance.ConveyorBeltRefs = ConveyorBeltRefs;
        ConveyorBeltHandler.Instance.Initialize();

        UIManager.Instance.UIRefs = UIRefs;

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
