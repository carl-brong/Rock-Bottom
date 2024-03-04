
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerTitle : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private UITitleScreen _titleScreen;
    [SerializeField] private UIPauseOptions _pauseOptions;
    [SerializeField] private UIPauseOptionsGame _pauseGame;
    [SerializeField] private UIPauseOptionsDisplay _pauseDisplay;
    [SerializeField] private UIPauseOptionsAudio _pauseAudio;
    [SerializeField] private UIPauseOptionsControls _pauseControls;
    private UIDataHandler _data;

    [Header("Gameplay")]
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private GameStateSO _gameStateManager;
    
    private void Awake()
    {
        _data = new UIDataHandler();
    }

    private void Start()
    {
        _gameStateManager.UpdateGameState(GameState.TitleScreen);
        _titleScreen.gameObject.SetActive(true);
        _inputReader.EnableMenuControls();
    }

    private void OnEnable()
    {
        _titleScreen.StartedGame += StartGame;
        _titleScreen.OptionsMenuOpened += OpenOptionsMenu;
        _titleScreen.ExitedGame += ExitGame;
    }

    private void OnDisable()
    {
        _titleScreen.StartedGame -= StartGame;
        _titleScreen.OptionsMenuOpened -= OpenOptionsMenu;
        _titleScreen.ExitedGame -= ExitGame;
    }
    
    private void OpenOptionsMenu()
    {
        // Input Reader Events
        _inputReader.PopMenuEvent += CloseOptionsMenu; // Allows the user to close the options menu
        
        // Main Events
        _titleScreen.StartedGame -= StartGame; // Disable Play Button
        _titleScreen.OptionsMenuOpened -= OpenOptionsMenu; // Disable Options Button
        _titleScreen.ExitedGame -= ExitGame; // Disable Exit Button
        
        // Options Events
        _pauseOptions.GameMenuOpened += OpenGameMenu; // Enable Game Button
        _pauseOptions.DisplayMenuOpened += OpenDisplayMenu; // Enable Display Button
        _pauseOptions.AudioMenuOpened += OpenAudioMenu; // Enable Audio Button
        _pauseOptions.ControlsMenuOpened += OpenControlMenu; // Enable Control Button
        
        // Display UI
        _pauseOptions.gameObject.SetActive(true);
        _titleScreen.gameObject.SetActive(false);
    }

    private void CloseOptionsMenu()
    {
        // Input Reader Events
        _inputReader.PopMenuEvent -= CloseOptionsMenu; // Stops the user from closing the options menu more than once
        
        // Main Events
        _titleScreen.StartedGame += StartGame; // Enable Play Button
        _titleScreen.OptionsMenuOpened += OpenOptionsMenu; // Enable Options Button
        _titleScreen.ExitedGame += ExitGame; // Enable Exit Button
        
        // Options Events
        _pauseOptions.GameMenuOpened -= OpenGameMenu; // Disables Game Button
        _pauseOptions.DisplayMenuOpened -= OpenDisplayMenu; // Disabled Display Button
        _pauseOptions.AudioMenuOpened -= OpenAudioMenu; // Disabled Audio Button
        _pauseOptions.ControlsMenuOpened -= OpenControlMenu; // Disable Control Button
        
        // Display UI
        _titleScreen.gameObject.SetActive(true);
        _pauseOptions.gameObject.SetActive(false);
    }

    private void OpenGameMenu()
    {
        // Input Reader Events
        _inputReader.PopMenuEvent -= CloseOptionsMenu; // Stops the user from closing the options menu while game menu is up
        _inputReader.PopMenuEvent += CloseGameMenu; // Allows the user to close the game menu
        
        // Options Events
        _pauseOptions.GameMenuOpened -= OpenGameMenu; // Disable Game Button
        _pauseOptions.DisplayMenuOpened -= OpenDisplayMenu; // Disable Display Button
        _pauseOptions.AudioMenuOpened -= OpenAudioMenu; // Disable Audio Button
        _pauseOptions.ControlsMenuOpened -= OpenControlMenu; // Disable Control Button
        
        // Game Events
        _pauseGame.DifficultySelected += _data.SetDifficulty;
        
        // Display UI
        _pauseGame.gameObject.SetActive(true);
        _pauseOptions.gameObject.SetActive(false);
    }

    private void CloseGameMenu()
    {
        // Input Reader Events
        _inputReader.PopMenuEvent -= CloseGameMenu; // Stops the user from closing the game menu more than once
        _inputReader.PopMenuEvent += CloseOptionsMenu; // Allows the user to close the options menu
        
        // Options Events
        _pauseOptions.GameMenuOpened += OpenGameMenu; // Enable Game Button
        _pauseOptions.DisplayMenuOpened += OpenDisplayMenu; // Enable Display Button
        _pauseOptions.AudioMenuOpened += OpenAudioMenu; // Enable Audio Button
        _pauseOptions.ControlsMenuOpened += OpenControlMenu; // Enable Control Button
        
        // Game Events
        _pauseGame.DifficultySelected -= _data.SetDifficulty;
        
        // Display UI
        _pauseOptions.gameObject.SetActive(true);
        _pauseGame.gameObject.SetActive(false);
    }

    private void OpenDisplayMenu()
    {
        // Input Reader Events
        _inputReader.PopMenuEvent -= CloseOptionsMenu; // Stops the user from closing the options menu when display is up
        _inputReader.PopMenuEvent += CloseDisplayMenu; // Allows the user to close the display menu
        
        // Options Events
        _pauseOptions.GameMenuOpened -= OpenGameMenu; // Disable Game Button
        _pauseOptions.DisplayMenuOpened -= OpenDisplayMenu; // Disable Display Button
        _pauseOptions.AudioMenuOpened -= OpenAudioMenu; // Disable Audio Button
        _pauseOptions.ControlsMenuOpened -= OpenControlMenu; // Disable Control Button
        
        // Display Events
        _pauseDisplay.WindowSelected += _data.SetWindow;
        _pauseDisplay.ResolutionSelected -= _data.SetResolution;
        
        // Display UI
        _pauseDisplay.gameObject.SetActive(true);
        _pauseOptions.gameObject.SetActive(false);
    }

    private void CloseDisplayMenu()
    {
        // Input Reader Events
        _inputReader.PopMenuEvent -= CloseDisplayMenu; // Stops the user from closing display more than once
        _inputReader.PopMenuEvent += CloseOptionsMenu; // Allows the user to close the options menu
        
        // Options Events
        _pauseOptions.GameMenuOpened += OpenGameMenu; // Enable Game Button
        _pauseOptions.DisplayMenuOpened += OpenDisplayMenu; // Enable Display Button
        _pauseOptions.AudioMenuOpened += OpenAudioMenu; // Enable Audio Button
        _pauseOptions.ControlsMenuOpened += OpenControlMenu; // Enable Control Button

        // Display Events
        _pauseDisplay.WindowSelected -= _data.SetWindow;
        _pauseDisplay.ResolutionSelected -= _data.SetResolution;
        
        // Display UI
        _pauseOptions.gameObject.SetActive(true);
        _pauseDisplay.gameObject.SetActive(false);
    }

    private void OpenAudioMenu()
    {
        // Input Reader Events
        _inputReader.PopMenuEvent -= CloseOptionsMenu; // Stops the user from closing the options menu while audio is up
        _inputReader.PopMenuEvent += CloseAudioMenu; // Allow the user to close the audio menu
        
        // Options Events
        _pauseOptions.GameMenuOpened -= OpenGameMenu; // Disable Game Button
        _pauseOptions.DisplayMenuOpened -= OpenDisplayMenu; // Disable Display Button
        _pauseOptions.AudioMenuOpened -= OpenAudioMenu; // Disable Audio Button
        _pauseOptions.ControlsMenuOpened -= OpenControlMenu; // Disable Control Button
        
        // Audio Events
        _pauseAudio.MasterAudioSet += _data.SetMasterAudio;
        _pauseAudio.MusicAudioSet += _data.SetMusicAudio;
        _pauseAudio.SFXAudioSet += _data.SetSFXAudio;
        
        // Display UI
        _pauseAudio.gameObject.SetActive(true);
        _pauseOptions.gameObject.SetActive(false);
    }

    private void CloseAudioMenu()
    {
        // Input Reader Events
        _inputReader.PopMenuEvent -= CloseAudioMenu; // Stops the user from closing the audio menu more than once
        _inputReader.PopMenuEvent += CloseOptionsMenu; // Allow the user to close the options menu
        
        // Options Events
        _pauseOptions.GameMenuOpened += OpenGameMenu; // Enable Game Button
        _pauseOptions.DisplayMenuOpened += OpenDisplayMenu; // Enable Display Button
        _pauseOptions.AudioMenuOpened += OpenAudioMenu; // Enable Audio Button
        _pauseOptions.ControlsMenuOpened += OpenControlMenu; // Enable Control Button
        
        // Audio Events
        _pauseAudio.MasterAudioSet -= _data.SetMasterAudio;
        _pauseAudio.MusicAudioSet -= _data.SetMusicAudio;
        _pauseAudio.SFXAudioSet -= _data.SetSFXAudio;
        
        // Display UI
        _pauseOptions.gameObject.SetActive(true);
        _pauseAudio.gameObject.SetActive(false);
    }

    private void OpenControlMenu()
    {
        // Input Reader Events
        _inputReader.PopMenuEvent -= CloseOptionsMenu; // Stops the user from closing the options menu while control is up
        _inputReader.PopMenuEvent += CloseControlMenu; // Allow the user to close the control menu
        
        // Options Events
        _pauseOptions.GameMenuOpened -= OpenGameMenu; // Disable Game Button
        _pauseOptions.DisplayMenuOpened -= OpenDisplayMenu; // Disable Display Button
        _pauseOptions.AudioMenuOpened -= OpenAudioMenu; // Disable Audio Button
        _pauseOptions.ControlsMenuOpened -= OpenControlMenu; // Disable Control Button
        
        // Control Events
        
        // Display UI
        _pauseControls.gameObject.SetActive(true);
        _pauseOptions.gameObject.SetActive(false);
    }
    
    private void CloseControlMenu()
    {
        // Input Reader Events
        _inputReader.PopMenuEvent -= CloseControlMenu; // Stops the user from closing the control menu more than once
        _inputReader.PopMenuEvent += CloseOptionsMenu; // Allow the user to close the options menu
        
        // Options Events
        _pauseOptions.GameMenuOpened += OpenGameMenu; // Enable Game Button
        _pauseOptions.DisplayMenuOpened += OpenDisplayMenu; // Enable Display Button
        _pauseOptions.AudioMenuOpened += OpenAudioMenu; // Enable Audio Button
        _pauseOptions.ControlsMenuOpened += OpenControlMenu; // Enable Control Button
        
        // Control Events
        
        // Display UI
        _pauseOptions.gameObject.SetActive(true);
        _pauseControls.gameObject.SetActive(false);
    }

    private void StartGame()
    {
        _gameStateManager.UpdateGameState(GameState.Gameplay);
        _inputReader.EnableGameplayControls();
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
