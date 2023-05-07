using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] GameObject _button_model;

    private IEnumerator _button_releasing;

    public void ButtonPressed(float buttonDefaultPosY, float buttonPressedPosY, float releaseSpeed)
    {
        stopReleasing();

        _button_model.transform.localPosition = new Vector3(_button_model. transform.localPosition.x, buttonPressedPosY, _button_model. transform.localPosition.z);


        _button_releasing = ButtonReleasing(buttonDefaultPosY, releaseSpeed);
        StartCoroutine(_button_releasing);
    }

    private IEnumerator ButtonReleasing(float buttonDefaultPosY, float releaseSpeed)
    {
        while(_button_model.transform.localPosition.y != buttonDefaultPosY)
        {
            _button_model.transform.localPosition = new Vector3(_button_model.transform.localPosition.x, Mathf.MoveTowards(_button_model.transform.localPosition.y, buttonDefaultPosY, releaseSpeed*Time.deltaTime), _button_model.transform.localPosition.z);
            yield return null;
        }

        yield break;
    }

    private void stopReleasing()
    {
        if(_button_releasing is not null)
            StopCoroutine(_button_releasing);
    }
}
