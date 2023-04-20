using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UIManager", menuName = "ScriptableObjects/Managers/UIManager")]
public class UIManager : SingletonSO<UIManager>, ISingleton, IEventObserver
{
    #region Singleton Variables
    private bool isDone = false;
    public bool IsDoneInitializing
    {
        get { return isDone; }
    }
    #endregion

    public void Initialize()
    {
        AddEventObservers();
        
        isDone = true;
    }


    public void AddEventObservers()
    {
        ;
    }
}
