


using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

// Vincent Lee
// 4/26/24

public class LevelSelectorMenu : BaseMenu
{
    public Button level1, level2, level3, level4, level5;

    public override void Awake()
    {
        base.Awake();
        level1.onClick.AddListener(() => GoToLevel(1));
        level2.onClick.AddListener(() => GoToLevel(2));
        level3.onClick.AddListener(() => GoToLevel(3));
        level4.onClick.AddListener(() => GoToLevel(4));
        level5.onClick.AddListener(() => GoToLevel(5));
    }

    private void GoToLevel(int level)
    {
        switch (level)
        {
            case 1:
                SceneManager.LoadScene(1);
                break;
            case 2:
                SceneManager.LoadScene(2);
                break;
            case 3:
                SceneManager.LoadScene(3);
                break;
            case 4:
                SceneManager.LoadScene(5);
                break;
            case 5:
                SceneManager.LoadScene(6);
                break;
        }
        inputReader.EnableGameplayControls();
        GameSingleton.Instance.UpdateGameState(GameState.Gameplay);
    }

}