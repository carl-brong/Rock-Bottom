
//Source: https://www.youtube.com/watch?v=KPaEnLpu57s

public class GameManager
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameManager();
            }
            
            return _instance;
        }
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
