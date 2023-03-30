using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Poolable : MonoBehaviour
{
    [SerializeField] protected ObjectPooling poolOrigin;
    public void SetObjectPool(ObjectPooling pool)
    {
        poolOrigin = pool;
    }
    public ObjectPooling GetObjectPool()
    {
        return this.poolOrigin;
    }

    public abstract void OnInstantiate();
    public abstract void OnActivate();
    public abstract void OnDeactivate();
}
