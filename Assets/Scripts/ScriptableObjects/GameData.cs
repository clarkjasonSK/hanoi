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

    [SerializeField] private bool _goal_pole_whole;
    public bool GoalPoalWhole
    {
        get { return _goal_pole_whole; }
        set { _goal_pole_whole = value; }
    }

    public void ResetGame()
    {
        _move_count = 0;
        _goal_pole_whole = false;
    }
}
