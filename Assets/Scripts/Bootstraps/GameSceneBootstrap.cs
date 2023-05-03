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
              initializeHandler(Instantiate(asset, HanoiSceneParent));

          }).Completed += (asyncOperation) =>
          {
              if (asyncOperation.Status == AsyncOperationStatus.Succeeded)
              {
                  Debug.Log("Async Assets loading finished!");
                  loadDependencies();

                  this.gameObject.SetActive(false);
                  EventBroadcaster.Instance.PostEvent(EventKeys.GAME_START, null);
              }
          };

    }

    private void initializeHandler(GameObject gameobjectParent)
    {
        if (gameobjectParent.GetComponent<Handler>() is null)
            return;

        //Debug.Log("found handler!");
        gameobjectParent.GetComponent<Handler>().Initialize();
    }

    private void loadDependencies()
    {
        //PoleHandler.Instance.Initialize();
        //RingHandler.Instance.Initialize();
        //PanelHandler.Instance.Initialize();
        //ConveyorBeltHandler.Instance.Initialize();
        //LeverHandler.Instance.Initialize();
        GameUIHandler.Instance.Initialize();
        //VFXHandler.Instance.Initialize();
        
        Debug.Log("Dependencies initialized!");
        

    }
}
