using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingController : MonoBehaviour
{
    #region Ring Controller Variables
    [SerializeField] private Rigidbody _rigidbody;

    #endregion

    void Start()
    {
        if (_rigidbody is null)
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
    }

    public void FloatRing()
    {
        _rigidbody.useGravity = true;
    }
    public void DropRing()
    {
        _rigidbody.useGravity = false;
    }
    public void DropRing(Vector3 newPos)
    {
        _rigidbody.useGravity = false;

    }
}
