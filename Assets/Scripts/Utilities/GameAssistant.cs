using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssistant : MonoBehaviour
{
    public delegate void ResetDelegate();

    private IEnumerator _reset_delay;

    #region Game Audio SFX
    [SerializeField] private AudioSource _audio_src;
    [SerializeField] private SimpleSFX _round_over_sfx;
    #endregion

    private void Start()
    {
        if (_audio_src is null)
        {
            _audio_src = GetComponent<AudioSource>();
        }
    }
    public void PlayRoundOverSFX()
    {
        _round_over_sfx.PlaySFX(_audio_src);
    }
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
