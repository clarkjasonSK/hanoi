using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameValues", menuName = "ScriptableObjects/GameValues")]
public class GameValues : ScriptableObject
{
    public float RingFloatSpeed;
    public float RingTravelSpeed;
    public float RingMaxDropSpeed;
    public float RingCollisionCooldown;

    public float PoleMoveSpeed;
    public float PoleShaftBase;
    public float PoleShaftMult;

    public float LeverRotateSpeed;

    public float PanelMoveSpeed;

    public float BeltMoveSpeed;

    public float GameRestartDelay;
}
