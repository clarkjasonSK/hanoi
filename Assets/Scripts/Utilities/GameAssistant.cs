using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssistant : MonoBehaviour
{
    public delegate void ResetDelegate();

    private IEnumerator _reset_delay;

    public void InvokeReset(ResetDelegate reset, float delay)
    {
        _reset_delay = invokeReset(reset, delay);
        StartCoroutine(_reset_delay);

    }


    private IEnumerator invokeReset(ResetDelegate reset, float delay)
    {
        yield return new WaitForSeconds(delay);
        reset?.Invoke();
    }
}
