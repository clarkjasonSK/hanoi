public class EventDictionary
{
    

}

public class EventKeys{
    public const string SYSTEM_START = "PROGRAM_STARTED";

    public const string GAME_START = "GAME_STARTED";
    public const string GAME_PAUSE = "GAME_PAUSED";
    public const string GAME_RESET = "GAME_RESET";

    public const string ASSETS_INIT = "ASSETS_INIT";
    public const string ASSETS_DESPAWNED = "ASSETS_DESPAWNED";
    public const string ASSETS_RESET = "ASSETS_RESET";
    public const string ASSETS_DISABLE = "ASSETS_DISABLE";
    public const string ASSETS_ENABLE = "ASSETS_ENABLE";


    public const string RINGS_SPAWN = "RINGS_SPAWN";
    public const string RING_DESPAWN = "RINGS_DESPAWN";
    public const string RING_SELECT = "RING_SELECTED";
    public const string RING_DESELECT = "RING_DESELECTED";
    public const string RING_DROPPED = "RING_DROPPED";
    public const string RING_STACKED_FULL= "RING_STACKED_FULL";


    public const string POLE_SPAWN = "POLE_SPAWN";
    public const string POLE_DESPAWN = "POLE_DESPAWN";
    public const string POLE_MOVE_FINISH = "POLE_MOVE_FINISH";
    public const string POLE_ADJUST = "POLE_ADJUST";

    public const string POLE_ADD_RING = "POLE_ADD_RING";
    public const string POLE_END_FULL = "POLE_FULL";

    public const string POS_PRESS = "POLE_PRESSED";
    public const string POS_ENTER = "POS_ENTER";


    public const string CON_BELT_MOVE = "CON_BELT_MOVE";
    public const string CON_BELT_STOP = "CON_BELT_STOP";


    public const string PANEL_DROP = "PANEL_DROP";
    public const string PANEL_RISE = "PANEL_RISE";


    public const string LEVER_POS_HOVER = "LEVER_POS_HOVER";
    public const string LEVER_POS_CHOSEN = "LEVER_POS_CHOSEN";


    public const string COUNT_UPDATE= "COUNT_UPDATE";
    public const string SLIDER_CHANGE = "SLIDER_CHANGE";

    public const string BUTTON_RESET_CLICKED = "BUTTON_RESET_CLICKED";


    public const string RING_HIT = "RING_HIT";
    public const string VFX_STOP = "VFX_STOP";



}

public class EventParamKeys
{
    public const string RING = "RING";

    public const string RING_HIT_SFX = "RING_HIT_SFX";
    public const string RING_HIT_VFX = "RING_HIT_VFX";

    public const string POLE = "POLE";

    public const string LEVER = "LEVER";
    public const string LEVER_POS = "LEVER_POS";

    public const string MOVE_COUNT = "MOVE_COUNT";
    public const string SLIDER_NUMBER = "SLIDER_NUMBER";

    public const string VFX_PARTICLE = "VFX_PARTICLE";
}
