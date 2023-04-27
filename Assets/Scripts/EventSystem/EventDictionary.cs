public class EventDictionary
{
    

}

public class EventKeys{
    public const string MENU_START = "PROGRAM_STARTED";

    public const string GAME_START = "GAME_STARTED";
    public const string GAME_PAUSE = "GAME_PAUSED";
    public const string GAME_RESET = "GAME_RESET";

    public const string DESPAWN_DONE = "DESPAWN_DONE";
    public const string ASSETS_RESET = "ASSETS_RESET";

    public const string RINGS_SPAWN = "RINGS_SPAWN";
    public const string RINGS_DESPAWN = "RINGS_DESPAWN";
    public const string RING_SELECT = "RING_SELECTED";
    public const string RING_DESELECT = "RING_DESELECTED";

    public const string RING_MOVE = "RING_MOVED";
    public const string RING_TOP_STACK = "RING_TOP_STACK";

    public const string POS_EXIT = "POS_EXIT";

    public const string POLE_MOVE_FINISH = "POLE_MOVE_FINISH";
    public const string POLE_PRESS = "POLE_PRESSED";
    public const string POLE_HOVER= "POLE_HOVERED";
    public const string POLE_ADD_RING = "POLE_ADD_RING";
    public const string POLE_FULL = "POLE_FULL";
    public const string POLE_DESPAWN= "POLE_DESPAWN";

    public const string PANEL_DROP = "PANEL_DROP";
    public const string PANEL_RISE = "PANEL_RISE";

    public const string LEVER_POS_HOVER = "LEVER_POS_HOVER";
    public const string LEVER_POS_CHOSEN = "LEVER_POS_CHOSEN";

    public const string COUNT_UPDATE= "COUNT_UPDATE";
    public const string SLIDER_CHANGE = "SLIDER_CHANGE";


}

public class EventParamKeys
{
    public const string RING = "RING"; 
    public const string RING_AMOUNT = "RING_AMOUNT";
    public const string RING_IS_SMALLEST = "RING_IS_SMALLEST";

    public const string POLE = "POLE";

    public const string LEVER = "LEVER";
    public const string LEVER_POS = "LEVER_POS";

    public const string MOVE_COUNT = "MOVE_COUNT";
    public const string SLIDER_NUMBER = "SLIDER_NUMBER";
}
