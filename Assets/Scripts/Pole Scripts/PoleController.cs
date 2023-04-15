using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleController : MonoBehaviour
{
    [SerializeField] private Pole _pole_ref;
    [SerializeField] private GameObject _pole_shaft;

    [SerializeField] private BoxCollider _base_collider;

    private IEnumerator _moving_pole;

    private void Start()
    {
        if (_pole_ref is null)
            _pole_ref = GetComponent<Pole>();
    }

    public void SetShaftHeight(float shaftHeight)
    {
        _pole_shaft.transform.localScale = new Vector3(_pole_shaft.transform.localScale.x, shaftHeight, _pole_shaft.transform.localScale.z);
    }

    public void MoveToLocation(Vector3 targetLocation, float moveSpeed)
    {
        _moving_pole = movingPole(targetLocation, moveSpeed);
        StartCoroutine(_moving_pole);
    }

    private IEnumerator movingPole(Vector3 targetLocation, float moveSpeed)
    {
        while(transform.localPosition.z!= targetLocation.z)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, Mathf.MoveTowards(transform.localPosition.z, targetLocation.z, moveSpeed*Time.deltaTime));
            yield return null;
        }
        _pole_ref.PoleMoveFinish();
        yield break;
    }
    public void ToggleColliders(bool toggle)
    {
        _base_collider.enabled = toggle;
    }

    public void ResetController()
    {
        StopCoroutine(_moving_pole);
    }
}
