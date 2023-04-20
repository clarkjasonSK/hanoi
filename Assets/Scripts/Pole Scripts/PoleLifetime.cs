using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleLifetime : MonoBehaviour
{
    [SerializeField] ObjectPooling _obj_pool;
    [SerializeField] Transform _pole_spawn_transform;

    public void StartPool()
    {
        _obj_pool.startPooling();
    }
    public Pole GetPole()
    {
        return _obj_pool.GameObjectPool.Get().GetComponent<Pole>();
    }

    public void ReturnPole(Pole pole)
    {
        _obj_pool.GameObjectPool.Release(pole.gameObject);
    }
}
