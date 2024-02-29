using UnityEngine;

//Source: https://www.youtube.com/watch?v=KPaEnLpu57s

public class PauseScript : MonoBehaviour
{
    public InputReader input;
    
    private void Awake()
    {
        input.PauseEvent += PauseGame;
        input.ResumeEvent += PauseGame;
    }

    private void OnDestroy()
    {
        input.PauseEvent -= PauseGame;
        input.ResumeEvent -= PauseGame;
    }

    public void PauseGame()
    {
        var currentState = GameManager.Instance.CurrentGameState;
        var newGameState = currentState == GameState.Gameplay
            ? GameState.Paused
            : GameState.Gameplay;
        GameManager.Instance.SetState(newGameState);
    }
}
