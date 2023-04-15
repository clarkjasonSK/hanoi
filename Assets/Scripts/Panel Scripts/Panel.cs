using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    [SerializeField] private PanelController _panel_ctrl;

    #region Ring Audio Variables
    [SerializeField] private AudioSource _audio_src;

    [SerializeField] private SimpleSFX _panel_drop;
    #endregion

    [SerializeField] private GameValues _game_values;
    private void Start()
    {
        if (_game_values is null)
            _game_values = GameManager.Instance.GameValues;

        if (_panel_ctrl is null)
            _panel_ctrl = GetComponent<PanelController>();


    }
    
    public void MovePanel(Transform targetPos)
    {
        _panel_drop.PlaySFX(_audio_src);
        _panel_ctrl.StartMoving(targetPos.localPosition.y, _game_values.PanelMoveSpeed);
    }

}
