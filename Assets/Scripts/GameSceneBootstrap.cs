using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GameSceneBootstrap : MonoBehaviour, IBootstrapper
{
    //[SerializeField] AssetReference HanoiScene;
    public void Awake()
    {
        LoadScene();
    }
    public void LoadScene()
    {
        loadDependencies();
        /*
        Addressables.LoadAssetAsync<GameObject>(HanoiScene).Completed += (asyncLoader) =>
          {
              if(asyncLoader.Status == AsyncOperationStatus.Succeeded)
              {
                  Instantiate(asyncLoader.Result);
                  asyncLoader.Result.SetActive(true);
              }
              else
              {
                  Debug.Log("Error loading!");
              }
          };*/

        
    }

    private void loadDependencies()
    {
        GetComponent<PoleRefs>().ObjectPooling.startPooling();

        PanelHandler.Instance.PanelRefs = GetComponent<PanelRefs>();
        PanelHandler.Instance.Initialize();

        ConveyorBeltHandler.Instance.ConveyorBeltRefs = GetComponent<ConveyorBeltRefs>();
        ConveyorBeltHandler.Instance.Initialize();

        PoleHandler.Instance.PoleRefs = GetComponent<PoleRefs>();
        PoleHandler.Instance.Initialize();

        RingHandler.Instance.RingRefs = GetComponent<RingRefs>();
        RingHandler.Instance.Initialize();

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
