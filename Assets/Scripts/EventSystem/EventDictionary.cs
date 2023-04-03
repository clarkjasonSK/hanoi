public class EventDictionary
{
    

}

public class EventKeys{
    public const string MENU_START = "PROGRAM_STARTED";

    public const string GAME_START = "GAME_STARTED";
    public const string GAME_PAUSE = "GAME_PAUSED";
    public const string GAME_RESET = "GAME_RESET";

    public const string POLE_PRESS = "POLE_PRESSED";
    public const string POLE_HOVER= "POLE_HOVERED";

    public const string RINGS_SPAWN = "RINGS_SPAWN";
    public const string RING_ADDPOLE = "RING_ADDPOLE";
    public const string RING_SELECT = "RING_SELECTED";
    public const string RING_DESELECT = "RING_DESELECTED";

    public const string RING_MOVE = "RING_MOVED";
    public const string RING_STACK = "RING_STACKED";

    public const string COUNT_UPDATE= "COUNT_UPDATE";
    public const string SLIDER_CHANGE = "SLIDER_CHANGE";

    public const string BEG_TOUCH = "BEGINNING_POSITION_TOUCHED";
    public const string MID_TOUCH = "MIDDLE_POSITION_TOUCHED";
    public const string END_TOUCH = "ENDING_POSITION_TOUCHED";
}

public class EventParamKeys
{
    public const string RING = "RING";
    public const string RING_AMOUNT = "RING_AMOUNT";

    public const string SELECTED_POLE = "SELECTED_POLE";
    public const string SELECTED_RING = "SELECTED_RING";

    public const string MOVE_COUNT = "MOVE_COUNT";
    public const string SLIDER_NUMBER = "SLIDER_NUMBER";
}
