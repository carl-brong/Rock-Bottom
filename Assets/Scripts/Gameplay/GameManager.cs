
//Source: https://www.youtube.com/watch?v=KPaEnLpu57s

public enum GameState
{
    Gameplay,
    Paused,
    GameOver
}

public class GameManager
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get { return _instance ??= new GameManager(); }
    }

    public GameState CurrentGameState { get; private set; }
    
    public delegate void GameStateChangeHandler(GameState newGameState);

    public event GameStateChangeHandler OnGameStateChanged;
    
    private GameManager()
    {
    }

    public void SetState(GameState newGameState)
    {
        if (newGameState == CurrentGameState)
        {
            return;
        }

        CurrentGameState = newGameState;
        OnGameStateChanged?.Invoke(newGameState);
    }
}
