using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI _move_count;

    [SerializeField] Slider _ring_slider;
    [SerializeField] TextMeshProUGUI _ring_count;

    #region Event Parameters
    private EventParameters _ui_param;
    #endregion

    void Start()
    {
        _ring_slider.onValueChanged.AddListener(
            delegate { OnSliderChanged(); } );

        _ui_param = new EventParameters();
        _move_count.text = Dictionary.MOVES + "0";

    }

    public void IncrCount(int moveCount)
    {
        _move_count.text = Dictionary.MOVES +""+ moveCount;
    }

    public void ResetCount()
    {
        _move_count.text = Dictionary.MOVES + "0";
    }

    void OnSliderChanged()
    {
        _ring_count.text = Dictionary.RING + ""+ _ring_slider.value;
        _ui_param.AddParameter<int>(EventParamKeys.SLIDER_NUMBER, (int)_ring_slider.value);

        EventBroadcaster.Instance.PostEvent(EventKeys.SLIDER_CHANGE, _ui_param);
    }
}
