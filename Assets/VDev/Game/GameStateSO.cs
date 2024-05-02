
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

// Vincent Lee
// 5/2/24

public enum GameState
{
    Gameplay,
    Paused,
    GameOver,
    TitleScreen
}

[CreateAssetMenu(menuName = "Gameplay/GameState", fileName = "GameStateSO", order = 0)]
public class GameStateSO : ScriptableObject
{
    public GameState CurrentGameState => _currentGameState;
    public event UnityAction<GameState> OnStateChange = delegate { };
    
    [Header("Game States")]
    [SerializeField][ReadOnly] private GameState _currentGameState;
    [SerializeField] [ReadOnly] private GameState _previousGameState;

    public void UpdateGameState(GameState newGameState)
    {
        if (newGameState == _currentGameState) return;
        _previousGameState = _currentGameState;
        _currentGameState = newGameState;
        OnStateChange.Invoke(newGameState);
    }

    public void ReturnToPreviousGameState()
    {
        if (_previousGameState == _currentGameState) return;
        (_previousGameState, _currentGameState) = (_currentGameState, _previousGameState);
        OnStateChange.Invoke(_currentGameState);
    }
    
}
