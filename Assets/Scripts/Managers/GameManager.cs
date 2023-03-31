using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    PROGRAM_START,
    MAIN_MENU,
    INGAME,
    PAUSED
}

public class GameManager : Singleton<GameManager>, ISingleton, IEventObserver
{
    #region ISingleton Variables
    private bool isDone = false;
    public bool IsDoneInitializing
    {
        get { return isDone; }
    }
    #endregion

    #region StateHandler Variables
    private StateHandler<GameState> _game_state_handler;
    public StateHandler<GameState> GameStateHandler
    {
        get { return _game_state_handler; }
    }
    public GameState GameState
    {
        get { return _game_state_handler.CurrentState; }
    }
    #endregion

    [SerializeField] private GameValues _game_values;
    public GameValues GameValues
    {
        get { return _game_values; }
    }

    [SerializeField] private GameData _game_data;
    public int RingAmount
    {
        get { return _game_data.RingsAmount; }
        set { _game_data.RingsAmount = value; }
    }
    

    public void Initialize()
    {
        _game_state_handler = new StateHandler<GameState>();
        _game_state_handler.Initialize(GameState.PROGRAM_START);

        _game_values = ScriptableObjectsHelper.GetSO<GameValues>(FileNames.GAME_VALUES);
        _game_data = ScriptableObjectsHelper.GetSO<GameData>(FileNames.GAME_DATA);

        AddEventObservers();


        isDone = true;
    }
    public void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.MENU_START, OnMenuStart);
        EventBroadcaster.Instance.AddObserver(EventKeys.GAME_START, OnGameStart);
        EventBroadcaster.Instance.AddObserver(EventKeys.GAME_PAUSE, OnGamePause);
    }
    

    #region Event Broadcaster Notifications
    public void OnMenuStart(EventParameters param=null)
    {
        _game_state_handler.Initialize(GameState.MAIN_MENU);
    }
    public void OnGameStart(EventParameters param = null)
    {
        _game_state_handler.Initialize(GameState.INGAME);


    }
    public void OnGamePause(EventParameters param = null)
    {
        _game_state_handler.Initialize(GameState.PAUSED);

    }
    
    #endregion
}

