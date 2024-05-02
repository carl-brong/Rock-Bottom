

using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

// Vincent Lee
// 4/26/24

public class GameSingleton : MonoBehaviour
{
    public static GameSingleton Instance { get; private set; }
    [SerializeField] private GameStateSO GameState;
    [SerializeField] private Player Player;

    public GameState CurrentGameState => GameState.CurrentGameState;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            if (transform.parent != null)
                transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void UpdateGameState(GameState newGameState)
    {
        GameState.UpdateGameState(newGameState);
    }

    public void ReturnToPrevious()
    {
        GameState.ReturnToPreviousGameState();
    }

    public void RestartCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Player.transform.position = Player.startpos;
        Player.Rb.velocity = Vector2.zero;
        Player.CurrentHealth = Player.MaxHealth;
    }
}
