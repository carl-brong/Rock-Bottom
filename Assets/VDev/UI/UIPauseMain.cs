
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPauseMain : MonoBehaviour
{
    [SerializeField] private GameObject _resumeObject, _restartObject, _optionsObject, _exitObject;
    [SerializeField] private Color _selectionColor;
    
    
    private Button _resumeButton, _restartButton, _optionsButton, _exitButton;
    
    
    public event UnityAction ResumedGame = delegate { };
    public event UnityAction RestartedGame = delegate { };
    public event UnityAction OptionsMenuOpened = delegate { };
    public event UnityAction ExitLevelRequested = delegate { };

    private void Awake()
    {
        _resumeButton = _resumeObject.GetComponent<Button>();
        _restartButton = _restartObject.GetComponent<Button>();
        _optionsButton = _optionsObject.GetComponent<Button>();
        _exitButton = _exitObject.GetComponent<Button>();
    }

    private void Start()
    {
        ChangeSelectionColor(_resumeButton, _selectionColor);
        ChangeSelectionColor(_restartButton, _selectionColor);
        ChangeSelectionColor(_optionsButton, _selectionColor);
        ChangeSelectionColor(_exitButton, _selectionColor);
        
    }

    private void OnEnable()
    {
        _resumeButton.onClick.AddListener(Resume);
        _restartButton.onClick.AddListener(Restart);
        _optionsButton.onClick.AddListener(OpenOptionsMenu);
        _exitButton.onClick.AddListener(ExitLevel);
        StartCoroutine(Delay());
    }

    private void OnDisable()
    {
        _resumeButton.onClick.RemoveListener(Resume);
        _restartButton.onClick.RemoveListener(Restart);
        _optionsButton.onClick.RemoveListener(OpenOptionsMenu);
        _exitButton.onClick.RemoveListener(ExitLevel);
    }

    public void Resume()
    {
        ResumedGame.Invoke();
    }

    public void Restart()
    {
        RestartedGame.Invoke();
    }

    private void OpenOptionsMenu()
    {
        OptionsMenuOpened.Invoke();
    }

    private void ExitLevel()
    {
        ExitLevelRequested.Invoke();
    }

    private void ChangeSelectionColor(Selectable button, Color color)
    {
        var block = button.colors;
        block.selectedColor = color;
        button.colors = block;
    }

    private IEnumerator Delay()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        _resumeButton.Select();
    }
}
