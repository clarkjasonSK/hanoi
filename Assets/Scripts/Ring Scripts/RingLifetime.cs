using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingLifetime : MonoBehaviour
{
    [SerializeField] List<GameObject> _ring_templates;
    [SerializeField] Transform _ring_spawn_transform;

    public Ring GetNewRing(int ringIndex)
    {
        _ring_templates[ringIndex].GetComponent<Ring>().OnActivate();
        return _ring_templates[ringIndex].GetComponent<Ring>();
    }

    public void ReleaseRing(Ring r)
    {
        r.OnDeactivate();
        r.gameObject.SetActive(false);
    }
}
