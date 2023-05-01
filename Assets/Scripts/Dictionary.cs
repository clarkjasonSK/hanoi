using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Dictionary
{
    public const string MOVES = "Moves: ";
    public const string RING = "Rings: ";

    public const float RING_VFX_BASE_RAD = 6.2f;
    public const float RING_VFX_DECREMENT = .7f;
}

public static class PoleDictionary
{
    public const int SPAWN_INDEX = 2;

    public const int SPAWN_POS = -1;
    public const int BEG_POS = 1;
    public const int END_POS = 3;

}
public static class TagNames
{
    public const string RING = "Ring";
    public const string POLE = "Pole";
    public const string DESPAWN = "DespawnArea";

    public const string LEVER_HANDLE = "LeverHandle";

    public const string HANOI_SCENE = "HanoiScene";
} 

public static class FileNames
{
    public const string SO_MANAGERS = "ScriptableObjects/Managers/";
    public const string SO_HANDLERS = "ScriptableObjects/Handlers/";
    public const string GAME_DATA = "ScriptableObjects/GameData";
    public const string GAME_VALUES = "ScriptableObjects/GameValues1";
}
