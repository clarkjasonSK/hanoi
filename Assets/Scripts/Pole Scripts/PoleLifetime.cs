using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleLifetime : MonoBehaviour
{
    [SerializeField] ObjectPooling _obj_pool;
    [SerializeField] GameObject _pole_template;
    [SerializeField] Transform _pole_spawn_transform;

    public Pole GetPole()
    {
        _obj_pool.ObjectTemplate = _pole_template;
        _obj_pool.ObjectTransform = _pole_spawn_transform;

        return _obj_pool.GameObjectPool.Get().GetComponent<Pole>();
    }

    public void ReturnPole(GameObject gameobject)
    {
        _obj_pool.GameObjectPool.Release(gameObject);
    }
}
