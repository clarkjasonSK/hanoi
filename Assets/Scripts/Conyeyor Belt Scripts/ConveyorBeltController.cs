using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltController : MonoBehaviour
{
    [SerializeField] Material _conveyor_mat;

    private IEnumerator _moving_belt;
    public void StartMoving(float moveSpeed)
    {
        _moving_belt = movingBelt(_conveyor_mat.mainTextureOffset.y + moveSpeed, moveSpeed);

        StartCoroutine(_moving_belt);
    }
    private IEnumerator movingBelt(float targetOffset, float moveSpeed)
    {
        while (_conveyor_mat.mainTextureOffset.y != targetOffset)
        {
            _conveyor_mat.mainTextureOffset = new Vector2(_conveyor_mat.mainTextureOffset.x, Mathf.MoveTowards(_conveyor_mat.mainTextureOffset.y, targetOffset, moveSpeed * Time.deltaTime));
            yield return null;
        }

        yield break;
    }

    public void StopMoving()
    {
        StopCoroutine(_moving_belt);
    }
}
