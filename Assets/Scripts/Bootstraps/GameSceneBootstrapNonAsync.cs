using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneBootstrapNonAsync : MonoBehaviour, IBootstrapper
{
    [SerializeField]  GameObject[] _hanoi_scene_assets;

    private void Awake()
    {
        LoadScene();
    }

    public void LoadScene()
    {
        for(int i=0; i< _hanoi_scene_assets.Length; i++)
        {
            initializeHandler(_hanoi_scene_assets[i]);
        }

        this.gameObject.SetActive(false);
        EventBroadcaster.Instance.PostEvent(EventKeys.GAME_START, null);
    }
    private void initializeHandler(GameObject gameobjectParent)
    {
        if (gameobjectParent.GetComponent<Handler>() is null)
            return;

        //Debug.Log("found handler!");
        gameobjectParent.GetComponent<Handler>().Initialize();
    }

}
