using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _move_count;
    [SerializeField] TextMeshProUGUI _ring_count;

    [SerializeField] Button _reset_btn;

    void Start()
    {
        _move_count.text = Dictionary.MOVES + "0";

        _reset_btn.onClick.AddListener(OnResetClicked);
    }

    public void IncrCount(int moveCount)
    {
        _move_count.text = Dictionary.MOVES +""+ moveCount;
    }

    public void ResetCount()
    {
        _move_count.text = Dictionary.MOVES + "0";
    }

    public void SetRingCount(int ringAmount)
    {
        _ring_count.text = Dictionary.RING + ringAmount;
    }
    public void ToggleButton(bool toggle)
    {
        _reset_btn.interactable = toggle;
    }

    #region OnClick Functions
    public void OnResetClicked()
    {
        UIManager.Instance.ResetButtonClicked();
    }

    #endregion

}
