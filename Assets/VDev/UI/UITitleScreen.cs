
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UITitleScreen : MonoBehaviour
{
    [SerializeField] private GameObject _playObject, _optionsObject, _exitObject;


    private Button _playButton, _optionsButton, _exitButton;
    
    public event UnityAction LevelSelectOpened = delegate { };
    public event UnityAction OptionsMenuOpened = delegate { };
    public event UnityAction ExitedGame = delegate { };

    private void Awake()
    {
        _playButton = _playObject.GetComponent<Button>();
        _optionsButton = _optionsObject.GetComponent<Button>();
        _exitButton = _exitObject.GetComponent<Button>();
    }

    private void OnEnable()
    {
        _playButton.onClick.AddListener(OpenLevelSelect);
        _optionsButton.onClick.AddListener(OpenOptions);
        _exitButton.onClick.AddListener(ExitGame);
        StartCoroutine(Delay());
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(OpenLevelSelect);
        _optionsButton.onClick.RemoveListener(OpenOptions);
        _exitButton.onClick.RemoveListener(ExitGame);
    }

    private void OpenLevelSelect()
    {
        LevelSelectOpened.Invoke();
    }

    private void OpenOptions()
    {
        OptionsMenuOpened.Invoke();
    }

    private void ExitGame()
    {
        ExitedGame.Invoke();
    }

    private IEnumerator Delay()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        _playButton.Select();
    }
}
