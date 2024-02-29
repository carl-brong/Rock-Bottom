using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public GameObject menu;
    public GameObject firstSelected;
    public EventSystem eventSystem;

    private void Awake()
    {
        GameManager.Instance.OnGameStateChanged += DisplayPauseMenu;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGameStateChanged -= DisplayPauseMenu;
    }

    private void DisplayPauseMenu(GameState newgamestate)
    {
        switch (newgamestate)
        {
            case GameState.Gameplay:
                menu.SetActive(false);
                break;
            case GameState.Paused:
                menu.SetActive(true);
                eventSystem.SetSelectedGameObject(firstSelected);
                break;
        }
    }
}
