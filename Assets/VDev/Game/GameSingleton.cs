

using System.ComponentModel;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

// Vincent Lee
// 4/26/24

public class GameSingleton : MonoBehaviour
{
    public static GameSingleton Instance { get; private set; }
    [FormerlySerializedAs("GameState")] [SerializeField] public GameStateSO GameStateSO;
    public Canvas gameOverMenu;

    public GameState CurrentGameState => GameStateSO.CurrentGameState;
    
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
        GameStateSO.UpdateGameState(newGameState);
        
        if (newGameState == GameState.GameOver)
        {
            Time.timeScale = 0;
            gameOverMenu.gameObject.SetActive(true);
        }
        
        
        
    }

    public void ReturnToPrevious()
    {
        GameStateSO.ReturnToPreviousGameState();
    }

    public void RestartCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        UpdateGameState(GameState.Gameplay);
        Time.timeScale = 1;
        var Player = GameObject.FindWithTag("Player")?.GetComponent<Player>();
        Player.transform.position = Player.startpos;
        Player.Rb.velocity = Vector2.zero;
        Player.CurrentHealth = Player.MaxHealth;
        Player.HealHealth(1);
    }

    public void SetDifficulty()
    {
        var obj = GameObject.Find("--CHECKPOINTS--");
        if (obj == null) return;
        for (var i = 0; i < obj.transform.childCount; i++)
        {
            obj.transform.GetChild(i).gameObject.SetActive((PlayerPrefs.GetInt("Difficulty", 0) != 1));
        }
    }

    public void ExitLevel()
    {
        CleanUp();
        UpdateGameState(GameState.TitleScreen);
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }

    private static void CleanUp()
    {
        var objs = FindObjectsOfType<PersistObject>();
        foreach (var obj in objs)
        {
            Destroy(obj.gameObject);
        }
    }
}
