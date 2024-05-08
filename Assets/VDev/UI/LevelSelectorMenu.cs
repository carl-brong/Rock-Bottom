


using UnityEngine;
using UnityEngine.SceneManagement;
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

    public override void OnEnable()
    {
        base.OnEnable();
        switch (PlayerPrefs.GetInt("Progress", 1))
        {
            case 1:
                level1.interactable = true;
                level2.interactable = false;
                level3.interactable = false;
                level4.interactable = false;
                level5.interactable = false;
                break;
            case 2:
                level1.interactable = true;
                level2.interactable = true;
                level3.interactable = false;
                level4.interactable = false;
                level5.interactable = false;
                break;
            case 3:
                level1.interactable = true;
                level2.interactable = true;
                level3.interactable = true;
                level4.interactable = false;
                level5.interactable = false;
                break;
            case 4:
                level1.interactable = true;
                level2.interactable = true;
                level3.interactable = true;
                level4.interactable = true;
                level5.interactable = false;
                break;
            case 5:
                level1.interactable = true;
                level2.interactable = true;
                level3.interactable = true;
                level4.interactable = true;
                level5.interactable = true;
                break;
        }
    }

    private void GoToLevel(int level)
    {
        switch (level)
        {
            case 1:
                Destroy(GameSingleton.Instance.gameObject);
                SceneManager.LoadScene(2);
                break;
            case 2:
                Destroy(GameSingleton.Instance.gameObject);
                SceneManager.LoadScene(4);
                break;
            case 3:
                Destroy(GameSingleton.Instance.gameObject);
                SceneManager.LoadScene(5);
                break;
            case 4:
                Destroy(GameSingleton.Instance.gameObject);
                SceneManager.LoadScene(7);
                break;
            case 5:
                Destroy(GameSingleton.Instance.gameObject);
                SceneManager.LoadScene(8);
                break;
        }

        inputReader.EnableGameplayControls();
        GameSingleton.Instance.UpdateGameState(GameState.Gameplay);
    }

}