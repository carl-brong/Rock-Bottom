using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelSelector : MonoBehaviour
{
    [SerializeField] private Button level1, level2, level3, level4, level5;

    public event UnityAction StartedGame = delegate { };
    
    private void Awake()
    {
        level1.onClick.AddListener(() => GoToLevel(1));
        level2.onClick.AddListener(() => GoToLevel(2));
        level3.onClick.AddListener(() => GoToLevel(3));
        level4.onClick.AddListener(() => GoToLevel(4));
        level5.onClick.AddListener(() => GoToLevel(5));
        ManageLocked(PlayerPrefs.GetInt("Progress", 1));
    }

    private void OnEnable()
    {
        ManageLocked(PlayerPrefs.GetInt("Progress", 1));
        StartCoroutine(DelayedSelect());
    }

    private IEnumerator DelayedSelect()
    {
        yield return new WaitForEndOfFrame();
        level1.Select();
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
        StartedGame.Invoke();
    }

    private void ManageLocked(int progress)
    {
        switch (progress)
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
}
