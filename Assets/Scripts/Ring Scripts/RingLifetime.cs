using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingLifetime : MonoBehaviour
{
    //[SerializeField] ObjectPooling _obj_pool;
    [SerializeField] List<GameObject> _ring_templates;
    [SerializeField] Transform _ring_spawn_transform;

    public Ring GetNewRing(int ringIndex)
    {
        //_obj_pool.ObjectTransform = _ring_spawn_transform;

        //_ring_templates[ringIndex].GetComponent<Ring>().OnActivate();
        _ring_templates[ringIndex].GetComponent<Ring>().OnActivate();
        return _ring_templates[ringIndex].GetComponent<Ring>();
    }
    /*
    private GameObject getRing(GameObject ringObject)
    {
        _obj_pool.ObjectTemplate = ringObject;
        return _obj_pool.GameObjectPool.Get();
    }*/

    public void ReleaseRing(Ring r)
    {
        r.OnDeactivate();
        r.gameObject.SetActive(false);
        //_obj_pool.GameObjectPool.Release(r.gameObject);
    }
}
