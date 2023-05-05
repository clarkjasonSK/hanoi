using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Dictionary
{
    public const string MOVES = "Moves: ";
    public const string RING = "Rings: ";

    public static Color MOVES_NEUTRAL = new Color32(175, 175, 175, 255); 
    public static Color MOVES_UNDER = new Color32(82, 195, 47, 255); 
    public static Color MOVES_OVER = new Color32(205, 58, 48, 255);
    public static Color MOVES_SUCESS = new Color32(229, 206, 49, 255);
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

    public const string RING_PARENT = "RingParent";
} 

public static class FileNames
{
    public const string SO_MANAGERS = "ScriptableObjects/Managers/";
    public const string SO_HANDLERS = "ScriptableObjects/Handlers/";
    public const string GAME_DATA = "ScriptableObjects/GameData";
    public const string GAME_VALUES = "ScriptableObjects/GameValues1";
    public const string VISUAL_VALUES = "ScriptableObjects/VisualValues1";
}
