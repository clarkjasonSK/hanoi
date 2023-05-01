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
        PoleHandler.Instance.Initialize(); // POLE BEFORE RING 
        RingHandler.Instance.Initialize();
        PanelHandler.Instance.Initialize();
        ConveyorBeltHandler.Instance.Initialize();
        LeverHandler.Instance.Initialize();
        GameUIHandler.Instance.Initialize();
        VFXHandler.Instance.Initialize();
        
        Debug.Log("Dependencies initialized!");
        this.gameObject.SetActive(false);
        EventBroadcaster.Instance.PostEvent(EventKeys.GAME_START, null);
        

    }
}
