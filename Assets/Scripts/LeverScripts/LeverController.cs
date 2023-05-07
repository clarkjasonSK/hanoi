using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    [SerializeField] private Lever _lever_ref;
    [SerializeField] private Transform _lever_handle;

    private IEnumerator _rotation;

    public void Initialize(Lever leverRef, Transform leverHandle)
    {
        _lever_ref = leverRef;
        _lever_handle = leverHandle;
    }
    public void StartRotating(int rotAngle, float rotSpeed)
    {
        StopRotating();

        _rotation = rotatingLever(rotAngle, rotSpeed);
        StartCoroutine(_rotation);
    }

    public void StopRotating()
    {
        if(_rotation is not null)
            StopCoroutine(_rotation);
    }
    private IEnumerator rotatingLever(int rotAngle, float rotSpeed)
    {
        while (LeverHelper.ConvertEulerAngle(_lever_handle.transform.localEulerAngles.x) != rotAngle)
        {
            _lever_handle.transform.localEulerAngles = new Vector3(Mathf.MoveTowards(LeverHelper.ConvertEulerAngle(_lever_handle.transform.localEulerAngles.x), rotAngle, rotSpeed * Time.deltaTime), 0, 0);
            yield return null;
        }

        _lever_ref.RotateFinish();
        yield break;
    }
}
