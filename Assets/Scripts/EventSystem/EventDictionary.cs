public class EventDictionary
{
    

}

public class EventKeys{
    public const string START_MENU = "PROGRAM_START";


    public const string START_GAME = "GAME_START";
    public const string PAUSE_GAME = "GAME_PAUSE";

    public const string PLAYER_HIT = "PLAYER_HIT";
    public const string PROJ_DESPAWN = "PROJECTILE_DESPAWN";


    //UI EVENT KEYS
    public const string PLAY_PRESSED = "PLAY_BUTTON_PRESSED";
    public const string LEVEL_PRESSED = "LEVEL_BUTTON_PRESSED";
    public const string LEVEL_BACK_PRESSED = "LEVEL_BACK_BUTTON_PRESSED";
}

public class EventParamKeys
{
    public const string PLAYER_PARAM = "PLAYER";
    public const string PROJ_PARAM = "PROJECTILE";

    public const string LEVEL_ID = "LEVEL_ID";

    //UI EVENT PARAM KEYS
    public const string MAIN_PANEL_PARAM = "MAIN_MENU_PANEL";
    public const string LEVEL_PANEL_PARAM = "LEVEL_PANEL";

    public const string TOP_PANEL_PARAM = "TOP_PANEL";
    public const string MID_PANEL_PARAM = "MID_PANEL";
    public const string BTM_PANEL_PARAM = "BOTTOM_PANEL";
}
