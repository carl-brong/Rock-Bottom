using UnityEngine;
using UnityEngine.EventSystems;

public class GameOverMenu : MonoBehaviour
{
    public GameObject menu;
    public GameObject firstSelected;
    public EventSystem eventSystem;

    private void Awake()
    {
        GameManager.Instance.OnGameStateChanged += DisplayGameOverMenu;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGameStateChanged -= DisplayGameOverMenu;
    }

    private void DisplayGameOverMenu(GameState newgamestate)
    {
        switch (newgamestate)
        {
            case GameState.Gameplay:
                menu.SetActive(false);
                break;
            case GameState.GameOver:
                menu.SetActive(true);
                eventSystem.SetSelectedGameObject(firstSelected);
                break;
        }
    }
}