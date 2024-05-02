

using UnityEngine;
using UnityEngine.UI;

// Vincent Lee
// 4/26/24

public class GameOverMenu : BaseMenu
{
    public Button retryButton, returnButton;
    public Canvas returnPrompt;
    
    public override void Awake()
    {
        base.Awake();
        retryButton.onClick.AddListener(Restart);
        returnButton.onClick.AddListener(ExitToTitle);
    }

    private void Restart()
    {
        
    }

    private void ExitToTitle()
    {
        gameObject.SetActive(false);
        returnPrompt.gameObject.SetActive(true);
    }
}
