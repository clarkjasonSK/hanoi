using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    [SerializeField] private BoxCollider _panel_collider;

    #region IEnumerators
    private IEnumerator _move_panel;
    #endregion

    void Start()
    {
        if (_panel_collider is null)
        {
            _panel_collider = GetComponent<BoxCollider>();
        }

    }

    public void TogglePanelCollider(bool toggle)
    {
        _panel_collider.enabled = toggle;
    }

    public void StartMoving(float moveLocation, float moveSpeed)
    {
        StopMoving();

        _move_panel = movePanel(moveLocation, moveSpeed);
        StartCoroutine(_move_panel);
    }

    private IEnumerator movePanel(float moveLocation, float moveSpeed)
    {
        //Debug.Log("y: " + transform.localPosition.y);
        while (transform.position.y != moveLocation)
        {
            //Debug.Log("moving panel!");
            //Debug.Log("moving y: " + transform.localPosition.y);
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.MoveTowards(transform.localPosition.y, moveLocation, moveSpeed * Time.deltaTime), transform.localPosition.z);
            yield return null;
        }

        yield break;
    }

    public void StopMoving()
    {
        if (_move_panel is not null)
            StopCoroutine(_move_panel);
    }

}
