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
        PanelHandler.Instance.Initialize();
        ConveyorBeltHandler.Instance.Initialize();
        PoleHandler.Instance.Initialize();
        RingHandler.Instance.Initialize();
        GameUIHandler.Instance.Initialize();

        if (RingHandler.Instance.IsDoneInitializing &&
            PoleHandler.Instance.IsDoneInitializing &&
            PanelHandler.Instance.IsDoneInitializing &&
            ConveyorBeltHandler.Instance.IsDoneInitializing &&
            GameUIHandler.Instance.IsDoneInitializing 
            )
        {
            Debug.Log("Game Scene initialized!");
            EventBroadcaster.Instance.PostEvent(EventKeys.GAME_START, null);
        }
    }

}
