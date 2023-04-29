using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UIManager", menuName = "ScriptableObjects/Managers/UIManager")]
public class UIManager : SingletonSO<UIManager>, IEventObserver
{
    public override void Initialize()
    {
        AddEventObservers();
        
        isDone = true;
    }


    public void AddEventObservers()
    {
        ;
    }
}
