using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameValues", menuName = "ScriptableObjects/GameValues")]
public class GameValues : ScriptableObject
{
    public float RingFloatSpeed;
    public float RingTravelSpeed;

    public float PoleMoveSpeed;
}
