using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    [SerializeField] private Transform _lever_handle;
    public Transform LeverHandle
    {
        set { _lever_handle = value; }
    }

    private IEnumerator _rotation;

    public void StartRotating(float rotAngle, float rotSpeed)
    {
        _rotation = rotatingLever(rotAngle, rotSpeed);
        StartCoroutine(_rotation);
    }

    public void StopRotating()
    {
        StopCoroutine(_rotation);
    }
    private IEnumerator rotatingLever(float rotAngle, float rotSpeed)
    {
        while(_lever_handle.transform.rotation.x != rotAngle)
        {
            // TODO quaternion rotate?
        }
        yield return null;
    }
}
