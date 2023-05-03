using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXLifetime : MonoBehaviour
{

    [SerializeField] private ObjectPooling _ring_simple_vfx_pool;
    [SerializeField] private GameObject _ring_fancy_vfx;

    public void StartPooling()
    {
        _ring_fancy_vfx = Instantiate(_ring_fancy_vfx, _ring_simple_vfx_pool.ObjectTransform);
        _ring_fancy_vfx.SetActive(false);
        _ring_simple_vfx_pool.startPooling();
    }
    public GameObject GetRingVFX(bool goalpolewhole)
    {
        if (goalpolewhole)
        {
            _ring_fancy_vfx.SetActive(true);
            return _ring_fancy_vfx;
        }

        return _ring_simple_vfx_pool.GameObjectPool.Get();
    }

    public void ReturnRingVFX(SimpleVFX ringVfx)
    {
        if(ringVfx.GetObjectPool() is null)
        {
            ringVfx.gameObject.SetActive(false);
            return;
        }

        _ring_simple_vfx_pool.GameObjectPool.Release(ringVfx.gameObject);
    }


}
