using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingController : MonoBehaviour
{
    #region Ring Controller Variables
    [SerializeField] private Rigidbody _rigidbody;

    #endregion

    #region IEnumerators
    private IEnumerator _move_ring;
    private IEnumerator _float_ring;
    #endregion

    void Start()
    {
        if (_rigidbody is null)
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

    }

    public void Reset()
    {
        ResetForces();
        StopMoving();
        StopFloating();
    }

    public void ResetForces()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    public void StartMoving(float moveLocation, float moveSpeed)
    {
        StopMoving();
        _move_ring = moveRing(moveLocation, moveSpeed);
        StartCoroutine(_move_ring);
    }

    private IEnumerator moveRing(float moveLocation, float moveSpeed)
    {
        while(transform.position.z != moveLocation)
        {
            Debug.Log("Moving ring!");
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, Mathf.MoveTowards(transform.localPosition.z, moveLocation, moveSpeed * Time.deltaTime));
            yield return null;
        }

        yield break;
    }

    public void StopMoving()
    {
        if(_move_ring is not null)
            StopCoroutine(_move_ring);
    }
    public void StopMoving(float stopLocation)
    {
        StopMoving();
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, stopLocation);
    }

    public void StartFloating(float floatHeight, float floatSpeed)
    {
        _rigidbody.useGravity = false;

        _float_ring = floatRing(floatHeight, floatSpeed);
        StartCoroutine(_float_ring);
    }

    private IEnumerator floatRing(float floatHeight, float floatSpeed)
    {
        while (transform.position.y != floatHeight)
        {
            Debug.Log("Floating ring!");
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.MoveTowards(transform.localPosition.y, floatHeight, floatSpeed * Time.deltaTime), transform.localPosition.z);
            yield return null;
        }

        yield break;
    }
    public void StopFloating()
    {
        _rigidbody.useGravity = true;
        if(_float_ring is not null)
            StopCoroutine(_float_ring);
    }
}
