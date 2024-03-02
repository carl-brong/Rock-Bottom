
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuPause : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _firstSelected;
    [SerializeField] private bool _debug;
    
    private void Awake()
    {
        _inputReader.PauseEvent += HandlePause;
        _inputReader.ResumeEvent += HandleResume;
    }

    private void OnDestroy()
    {
        _inputReader.PauseEvent -= HandlePause;
        _inputReader.ResumeEvent -= HandleResume;
    }

    private void HandlePause()
    {
        MenuController.Instance.PushMenu(_pauseMenu, _firstSelected);
        Log($"Pushed {_pauseMenu}");
        EventSystem.current.SetSelectedGameObject(_firstSelected);
        
        var currentState = GameManager.Instance.CurrentGameState;
        if (currentState == GameState.Paused) return;
        GameManager.Instance.SetState(GameState.Paused);
    }

    private void HandleResume()
    {
        if (GameManager.Instance.CurrentGameState == GameState.GameOver) return;
        Log($"Popped {MenuController.Instance.PeekMenu().Menu.name}");
        MenuController.Instance.PopMenu();
        if (!MenuController.Instance.IsEmpty())
        {
            EventSystem.current.SetSelectedGameObject(MenuController.Instance.PeekMenu().FirstSelected);
            return;
        }
        GameManager.Instance.SetState(GameState.Gameplay);
    }

    private void Log(string message)
    {
        if (_debug) Debug.Log(message);
    }
}
