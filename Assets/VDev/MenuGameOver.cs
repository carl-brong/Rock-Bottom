using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuGameOver : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private GameObject _firstSelected;
    [SerializeField] private bool _debug;
    
    private void Awake()
    {
        GameManager.Instance.OnGameStateChanged += DisplayGameOverMenu;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGameStateChanged -= DisplayGameOverMenu;
    }

    private void DisplayGameOverMenu(GameState newGameState)
    {
        var currentState = GameManager.Instance.CurrentGameState;
        if (currentState != GameState.GameOver) return;
        
        MenuController.Instance.PushMenu(_gameOverMenu, _firstSelected);
        Log($"Pushed {_gameOverMenu}");
        
        EventSystem.current.SetSelectedGameObject(_firstSelected);

    }
    
    private void Log(string message)
    {
        if (_debug) Debug.Log(message);
    }
}
