

using System;
using UnityEngine;
using UnityEngine.UI;

// Vincent Lee
// 4/26/24

public class PauseMenu : BaseMenu
{
    public Button resumeButton, restartButton, optionsButton, returnButton;
    public Canvas optionsMenu, returnPrompt;

    public override void Awake()
    {
        base.Awake();
        resumeButton.onClick.AddListener(Resume);
        restartButton.onClick.AddListener(Restart);
        optionsButton.onClick.AddListener(OpenOptions);
        returnButton.onClick.AddListener(ExitToTitle);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        inputReader.EnableMenuControls();
        inputReader.PopMenuEvent += Resume;
        Time.timeScale = 0;
        GameSingleton.Instance.UpdateGameState(GameState.Paused);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        inputReader.PopMenuEvent -= Resume;
    }

    private void Resume()
    {
        gameObject.SetActive(false);
        inputReader.EnableGameplayControls();
        Time.timeScale = 1;
        GameSingleton.Instance.UpdateGameState(GameState.Gameplay);
    }

    private void Restart()
    {
        gameObject.SetActive(false);
        inputReader.EnableGameplayControls();
        Time.timeScale = 1;
        GameSingleton.Instance.RestartCurrentLevel();
    }
    
    private void OpenOptions()
    {
        gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(true);
    }
    
    private void ExitToTitle()
    {
        gameObject.SetActive(false);
        returnPrompt.gameObject.SetActive(true);
    }
}
