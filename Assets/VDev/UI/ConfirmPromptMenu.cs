

using UnityEngine;
using UnityEngine.UI;


// Vincent Lee
// 5/1/24

public class ConfirmPromptMenu : BaseMenu
{
    public Button yesButton, noButton;

    public override void Awake()
    {
        base.Awake();
        yesButton.onClick.AddListener(() =>
        {
            if (GameSingleton.Instance.CurrentGameState == GameState.TitleScreen)
            {
                Application.Quit();
            }
            GameSingleton.Instance.ExitLevel();
            Time.timeScale = 1;
        });
        noButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            prevMenu.gameObject.SetActive(true);
        });

    }
}
