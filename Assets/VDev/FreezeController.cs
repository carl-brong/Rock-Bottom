
using UnityEngine;

public class FreezeController : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.OnGameStateChanged += HandleFreeze;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGameStateChanged -= HandleFreeze;
    }

    private void HandleFreeze(GameState newgamestate)
    {
        Debug.Log(newgamestate);
        Time.timeScale = newgamestate switch
        {
            GameState.Gameplay => 1,
            _ => 0
        };
    }
}