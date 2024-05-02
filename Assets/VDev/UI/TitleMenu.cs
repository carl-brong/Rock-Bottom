


using UnityEngine;
using UnityEngine.UI;

// Vincent Lee
// 4/26/24

public class TitleMenu : BaseMenu
{
    public Button playButton, optionsButton, exitButton;
    public Canvas levelSelectorMenu, optionsMenu, exitGamePrompt;

    public override void Awake()
    {
        base.Awake();
        playButton.onClick.AddListener(OpenLevelSelector);
        optionsButton.onClick.AddListener(OpenOptions);
        exitButton.onClick.AddListener(ExitGame);

        GameSingleton.Instance.UpdateGameState(GameState.TitleScreen);
    }

    private void OpenLevelSelector()
    {
        gameObject.SetActive(false);
        levelSelectorMenu.gameObject.SetActive(true);
    }

    private void OpenOptions()
    {
        gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(true);
    }

    private void ExitGame()
    {
        gameObject.SetActive(false);
        exitGamePrompt.gameObject.SetActive(true);
    }
}