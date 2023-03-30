using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData")]
public class GameData : ScriptableObject
{
    [SerializeField] [Range(3, 7)] private int _rings_amount = 3;
    public int RingsAmount
    {
        get { return _rings_amount; }
        set { _rings_amount = value; }
    }

    [SerializeField] private int _move_count;
    public int MoveCount
    {
        get { return _move_count; }
        set { _move_count = value; }
    }

    public void ResetGame(int ringAmount)
    {
        _rings_amount = ringAmount;
        _move_count = 0;
    }
}
