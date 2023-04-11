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



    #region Event Paramters
    private EventParameters _game_params;
    #endregion


    public void Initialize()
    {
        _game_state_handler = new StateHandler<GameState>();
        _game_state_handler.Initialize(GameState.PROGRAM_START);

        _game_values = ScriptableObjectsHelper.GetSO<GameValues>(FileNames.GAME_VALUES);
        _game_data = ScriptableObjectsHelper.GetSO<GameData>(FileNames.GAME_DATA);

        _game_data.RingsAmount = 3;
        _game_data.ResetGame();
        _game_params = new EventParameters();
        AddEventObservers();

        isDone = true;
    }
    public void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.MENU_START, OnMenuStart);
        EventBroadcaster.Instance.AddObserver(EventKeys.GAME_START, OnGameStart);
        EventBroadcaster.Instance.AddObserver(EventKeys.GAME_PAUSE, OnGamePause);

        EventBroadcaster.Instance.AddObserver(EventKeys.GAME_RESET, OnGameReset);

        EventBroadcaster.Instance.AddObserver(EventKeys.RING_MOVE, OnRingMove);
        EventBroadcaster.Instance.AddObserver(EventKeys.RING_TOP_STACK, OnRingTopStack);
        EventBroadcaster.Instance.AddObserver(EventKeys.POLE_FULL, OnEndPoleFull);
    }
    
    public void SetRingsAmount(int ringsAmount)
    {
        _game_data.RingsAmount = ringsAmount;
    }

    #region Event Broadcaster Notifications
    public void OnMenuStart(EventParameters param=null)
    {
        _game_state_handler.Initialize(GameState.MAIN_MENU);
    }
    public void OnGameStart(EventParameters param = null)
    {
        _game_state_handler.Initialize(GameState.INGAME);

        //EventBroadcaster.Instance.PostEvent(EventKeys.GAME_RESET, param);

    }
    public void OnGamePause(EventParameters param = null)
    {
        _game_state_handler.Initialize(GameState.PAUSED);

    }
    public void OnRingMove(EventParameters param = null)
    {
        _game_data.MoveCount++;
        _game_params.AddParameter(EventParamKeys.MOVE_COUNT, _game_data.MoveCount);
        EventBroadcaster.Instance.PostEvent(EventKeys.COUNT_UPDATE, _game_params);

    }
    public void OnGameReset(EventParameters param = null)
    {
        ResetGame();
    }
    public void OnEndPoleFull(EventParameters param = null)
    {
        _game_data.GoalPoalWhole = true;
    }
    public void OnRingTopStack(EventParameters param = null)
    {
        if (_game_data.GoalPoalWhole)
        {
            Invoke("ResetGame", _game_values.GameRestartDelay);
        }
    }
    private void ResetGame()
    {
        _game_data.ResetGame();
        EventBroadcaster.Instance.PostEvent(EventKeys.ASSETS_RESET, null);
        EventBroadcaster.Instance.PostEvent(EventKeys.RINGS_SPAWN, null);
    }
    
    #endregion
}

