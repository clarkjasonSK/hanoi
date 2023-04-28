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

[CreateAssetMenu(fileName = "GameManager", menuName = "ScriptableObjects/Managers/GameManager")]
public class GameManager : SingletonSO<GameManager>, ISingleton, IEventObserver
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


    #region Game Manager Variables
    [SerializeField] private GameValues _game_values;
    public GameValues GameValues
    {
        get { return _game_values; }
    }

    [SerializeField] private GameData _game_data;
    public int RingAmount
    {
        get { return _game_data.RingsAmount; }
    }
    public int MoveCount
    {
        get { return _game_data.MoveCount; }
    }
    public bool GoalPoleWhole
    {
        get { return _game_data.GoalPoleWhole; }
    }


    [SerializeField] private GameObject _game_assistant_prefab;
    [SerializeField] private GameAssistant _game_assistant;
    #endregion


    public void Initialize()
    {
        if(_game_values is null )
            _game_values = ScriptableObjectsHelper.GetSO<GameValues>(FileNames.GAME_VALUES);
        if(_game_data is null)
            _game_data = ScriptableObjectsHelper.GetSO<GameData>(FileNames.GAME_DATA);

        DontDestroyOnLoad(_game_assistant = Instantiate(_game_assistant_prefab).GetComponent<GameAssistant>());

        _game_state_handler = new StateHandler<GameState>();
        _game_state_handler.Initialize(GameState.PROGRAM_START);


        _game_data.RingsAmount = 3;
        _game_data.ResetGame();
        AddEventObservers();

        isDone = true;
    }
    public void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.SYSTEM_START, OnMenuStart);
        EventBroadcaster.Instance.AddObserver(EventKeys.GAME_START, OnGameStart);
        EventBroadcaster.Instance.AddObserver(EventKeys.GAME_PAUSE, OnGamePause);

        EventBroadcaster.Instance.AddObserver(EventKeys.GAME_RESET, OnGameReset);
        EventBroadcaster.Instance.AddObserver(EventKeys.ASSETS_DESPAWNED, OnAssetsDespawn);

    }
    
    public void SetRingsAmount(int ringsAmount)
    {
        _game_data.RingsAmount = ringsAmount;
    }
    public void IncrMoveCount()
    {
        _game_data.MoveCount++;

    }
    public void SetEndPoleFull()
    {
        _game_data.GoalPoleWhole = true;
    }

    private void resetGame()
    {
        _game_data.ResetGame();
        EventBroadcaster.Instance.PostEvent(EventKeys.ASSETS_RESET, null);
        EventBroadcaster.Instance.PostEvent(EventKeys.RINGS_SPAWN, null);
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
    public void OnGameReset(EventParameters param = null)
    {
        resetGame();
    }

    public void OnAssetsDespawn(EventParameters param = null)
    {
        _game_assistant.InvokeReset(resetGame, _game_values.GameRestartDelay);
    }
    
    #endregion
}

