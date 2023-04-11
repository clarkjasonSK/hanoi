using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleLifetime : MonoBehaviour
{
    [SerializeField] ObjectPooling _obj_pool;
    [SerializeField] Transform _pole_spawn_transform;

    public Pole GetPole()
    {
        return _obj_pool.GameObjectPool.Get().GetComponent<Pole>();
    }

    public void ReturnPole(GameObject gameobject)
    {
        _obj_pool.GameObjectPool.Release(gameObject);
    }
}
