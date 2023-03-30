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

        if (true)
        {
            Debug.Log(SceneNames.GAME_SCENE + " initialized!");
            //EventBroadcaster.Instance.PostEvent(EventKeys.START_GAME, null);
        }
    }
}
