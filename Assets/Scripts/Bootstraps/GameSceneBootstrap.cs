using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GameSceneBootstrap : MonoBehaviour, IBootstrapper
{
    [SerializeField] private AssetLabelReference HanoiScene;
    [SerializeField] private Transform HanoiSceneParent;

    public void Awake()
    {
        LoadScene();
    }
    public void LoadScene()
    {
        Addressables.LoadAssetsAsync<GameObject>(HanoiScene, (asset) =>
          {
              if (asset is null)
              {
                  Debug.Log("Error loading!");
                  return;
              }

              //Debug.Log("Insantiating: " + asset.name);
              Instantiate(asset, HanoiSceneParent);


          }).Completed += (asyncOperation) =>
          {
              if (asyncOperation.Status == AsyncOperationStatus.Succeeded)
              {
                  Debug.Log("Async Assets loading finished!");
                  loadDependencies();
              }
          };

    }

    private void loadDependencies()
    {
        PanelHandler.Instance.Initialize();
        ConveyorBeltHandler.Instance.Initialize();
        PoleHandler.Instance.Initialize();
        RingHandler.Instance.Initialize();
        LeverHandler.Instance.Initialize();
        GameUIHandler.Instance.Initialize();

        if (RingHandler.Instance.IsDoneInitializing &&
            PoleHandler.Instance.IsDoneInitializing &&
            PanelHandler.Instance.IsDoneInitializing &&
            ConveyorBeltHandler.Instance.IsDoneInitializing &&
            GameUIHandler.Instance.IsDoneInitializing
            )
        {
            Debug.Log("Dependencies initialized!");
            this.gameObject.SetActive(false);
            EventBroadcaster.Instance.PostEvent(EventKeys.GAME_START, null);
        }

    }
}
