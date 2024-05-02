

using UnityEngine;
using UnityEngine.UI;

// Vincent Lee
// 4/26/24

public class OptionsMenu : BaseMenu
{
    public Button gameButton, displayButton, audioButton, controlsButton;
    public Canvas gameMenu, displayMenu, audioMenu, controlsMenu;

    public override void Awake()
    {
        base.Awake();
        gameButton.onClick.AddListener(OpenGame);
        displayButton.onClick.AddListener(OpenDisplay);
        audioButton.onClick.AddListener(OpenAudio);
        controlsButton.onClick.AddListener(OpenControls);
    }

    private void OpenGame()
    {
        gameObject.SetActive(false);
        gameMenu.gameObject.SetActive(true);
    }

    private void OpenDisplay()
    {
        gameObject.SetActive(false);
        displayMenu.gameObject.SetActive(true);
    }

    private void OpenAudio()
    {
        gameObject.SetActive(false);
        audioMenu.gameObject.SetActive(true);
    }

    private void OpenControls()
    {
        gameObject.SetActive(false);
        controlsMenu.gameObject.SetActive(true);
    }
}
