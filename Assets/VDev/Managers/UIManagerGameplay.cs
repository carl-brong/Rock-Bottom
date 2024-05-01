
using UnityEngine;

public class UIManagerGameplay : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private UIPauseMain _pauseMain;
    [SerializeField] private UIPauseOptions _pauseOptions;
    [SerializeField] private UIPauseOptionsGame _pauseGame;
    [SerializeField] private UIPauseOptionsDisplay _pauseDisplay;
    [SerializeField] private UIPauseOptionsAudio _pauseAudio;
    [SerializeField] private UIPauseOptionsControls _pauseControls;
    [SerializeField] private UIGameOver _gameOver;
    private UIDataHandler _data;

    [Header("Gameplay")]
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private GameStateSO _gameStateManager;

    private void Awake()
    {
        _data = new UIDataHandler();
    }

    private void OnEnable()
    {
        _inputReader.PauseEvent += OpenPauseMenu;
        _gameStateManager.OnStateChange += OpenGameOverMenu;
    }

    private void OnDisable()
    {
        _inputReader.PauseEvent -= OpenPauseMenu;
        _gameStateManager.OnStateChange -= OpenGameOverMenu;
    }

    #if UNITY_EDITOR
    public void TestOpenPause()
    {
        OpenPauseMenu();
    }
    #endif
    
    private void OpenPauseMenu()
    {
        // Input Reader Events
        _inputReader.PauseEvent -= OpenPauseMenu; // Stops the user from opening the pause menu more than once
        _inputReader.PopMenuEvent += ClosePauseMenu; // Allows the user to exit with ESCAPE by default
        
        // Main Events
        _pauseMain.ResumedGame += ClosePauseMenu; // Enable Resume Button
        _pauseMain.RestartedGame += ClosePauseMenu; // Enable Restart Button
        _pauseMain.OptionsMenuOpened += OpenOptionsMenu; // Enable Options Button
        _pauseMain.ExitLevelRequested += ExitToTitleFromPause; // Enable Exit Button
        
        // Stop Time
        Time.timeScale = 0;
        
        // Switch to Menu Controls and update GameState
        _inputReader.EnableMenuControls();
        _gameStateManager.UpdateGameState(GameState.Paused);
        
        // Display UI
        _pauseMain.gameObject.SetActive(true);
        
    }
    
    private void ClosePauseMenu()
    {
        // Input Reader Events
        _inputReader.PauseEvent += OpenPauseMenu; // Allows the user to open the pause
        _inputReader.PopMenuEvent -= ClosePauseMenu; // Stops the user from closing nothing
        
        // Main Events
        _pauseMain.ResumedGame -= ClosePauseMenu; // Disable Resume Button
        _pauseMain.RestartedGame -= ClosePauseMenu; // Disable Restart Button
        _pauseMain.OptionsMenuOpened -= OpenOptionsMenu; // Disable Options Button
        _pauseMain.ExitLevelRequested -= ExitToTitleFromPause; // Disable Exit Button
        
        // Resume Time
        Time.timeScale = 1;
        
        // Return to Gameplay Controls and update Game State
        _inputReader.EnableGameplayControls();
        _gameStateManager.UpdateGameState(GameState.Gameplay);
        
        // Close UI
        _pauseMain.gameObject.SetActive(false);
    }

    private void ExitToTitleFromPause()
    {
        // Input Reader Events
        _inputReader.PopMenuEvent -= ClosePauseMenu; // Stops the user from closing nothing
        
        // Main Events
        _pauseMain.ResumedGame -= ClosePauseMenu; // Disable Resume Button
        _pauseMain.RestartedGame -= ClosePauseMenu; // Disable Restart Button
        _pauseMain.OptionsMenuOpened -= OpenOptionsMenu; // Disable Options Button
        _pauseMain.ExitLevelRequested -= ExitToTitleFromPause; // Disable Exit Button
        
        // Resume Time
        Time.timeScale = 1;
        
        _gameStateManager.UpdateGameState(GameState.TitleScreen);
        
        // Close UI
        _pauseMain.gameObject.SetActive(false);
    }
    
    private void OpenOptionsMenu()
    {
        // Input Reader Events
        _inputReader.PopMenuEvent -= ClosePauseMenu; // Stops the user from closing the pause menu while options is up
        _inputReader.PopMenuEvent += CloseOptionsMenu; // Allows the user to close the options menu
        
        // Main Events
        _pauseMain.ResumedGame -= ClosePauseMenu; // Disable Resume Button
        _pauseMain.RestartedGame -= ClosePauseMenu; // Disable Restart Button
        _pauseMain.OptionsMenuOpened -= OpenOptionsMenu; // Disable Options Button
        _pauseMain.ExitLevelRequested -= ClosePauseMenu; // Disable Exit Button
        
        // Options Events
        _pauseOptions.GameMenuOpened += OpenGameMenu; // Enable Game Button
        _pauseOptions.DisplayMenuOpened += OpenDisplayMenu; // Enable Display Button
        _pauseOptions.AudioMenuOpened += OpenAudioMenu; // Enable Audio Button
        _pauseOptions.ControlsMenuOpened += OpenControlMenu; // Enable Control Button
        
        // Display UI
        _pauseOptions.gameObject.SetActive(true);
        _pauseMain.gameObject.SetActive(false);
    }

    private void CloseOptionsMenu()
    {
        // Input Reader Events
        _inputReader.PopMenuEvent -= CloseOptionsMenu; // Stops the user from closing the options menu more than once
        _inputReader.PopMenuEvent += ClosePauseMenu; // Allows the user to close the pause menu
        
        // Main Events
        _pauseMain.ResumedGame += ClosePauseMenu; // Enable Resume Button
        _pauseMain.RestartedGame += ClosePauseMenu; // Enable Restart Button
        _pauseMain.OptionsMenuOpened += OpenOptionsMenu; // Enable Options Button
        _pauseMain.ExitLevelRequested += ClosePauseMenu; // Enable Exit Button
        
        // Options Events
        _pauseOptions.GameMenuOpened -= OpenGameMenu; // Disables Game Button
        _pauseOptions.DisplayMenuOpened -= OpenDisplayMenu; // Disabled Display Button
        _pauseOptions.AudioMenuOpened -= OpenAudioMenu; // Disabled Audio Button
        _pauseOptions.ControlsMenuOpened -= OpenControlMenu; // Disable Control Button
        
        // Display UI
        _pauseMain.gameObject.SetActive(true);
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

    private void OpenGameOverMenu(GameState state)
    {
        if (state != GameState.GameOver) return;
        
        // Input Reader Events
        _inputReader.PauseEvent -= OpenPauseMenu;
        
        // Game State Events
        _gameStateManager.OnStateChange -= OpenGameOverMenu;
        
        // Game Over Events
        _gameOver.RestartedGame += CloseGameOverMenu;
        _gameOver.ExitLevelRequested += CloseGameOverMenu;
        
        // Stop Time
        Time.timeScale = 0;
        
        // Switch to Menu Controls and update GameState
        _inputReader.EnableMenuControls();

        // Display UI
        _gameOver.gameObject.SetActive(true);
    }

    public void CloseGameOverMenu(int type)
    {
        // Input Reader Events
        _inputReader.PauseEvent -= OpenPauseMenu;
        
        // Game State Events
        _gameStateManager.OnStateChange += OpenGameOverMenu;
        
        // Game Over Events
        _gameOver.RestartedGame -= CloseGameOverMenu;
        _gameOver.ExitLevelRequested -= CloseGameOverMenu;
        
        // Start Time
        Time.timeScale = 1;
        
        if (type == 0)
        {
            _inputReader.EnableGameplayControls();
            _gameStateManager.UpdateGameState(GameState.Gameplay);
        }
        else
        {
            _gameStateManager.UpdateGameState(GameState.TitleScreen);
        }
        
        // Display UI
        _gameOver.gameObject.SetActive(false);
    }
}
