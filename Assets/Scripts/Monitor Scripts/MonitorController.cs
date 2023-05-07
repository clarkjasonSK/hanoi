using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonitorController : MonoBehaviour, IResettable
{
    [SerializeField] private TextMeshPro _moves_current;
    [SerializeField] private TextMeshPro _moves_least;

    public void Reset()
    {
        _moves_current.text = "" + 0;
        _moves_current.color = Dictionary.MOVES_NEUTRAL;
        _moves_least.text = "" + 0;
    }

    public void CountSet(int count)
    {
        _moves_current.text = "" + count;
    }

    public void ColorSet(Color targetColor)
    {
        if (compareColors(targetColor, _moves_current.color))
            return;

        _moves_current.color = targetColor;
    }

    public void LeastSet(int least)
    {
        _moves_least.text = "" + least;
    }

    private bool compareColors(Color32 targetColor, Color32 currentColor)
    {
        if(currentColor.Equals(targetColor))
        {
            return true;
        }
        return false;
    }
}
