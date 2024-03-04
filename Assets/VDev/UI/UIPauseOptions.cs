
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPauseOptions : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject, _displayObject, _audioObject, _controlsObject;
    [SerializeField] private Color _selectionColor;
    
    private Button _gameButton, _displayButton, _audioButton, _controlsButton;

    
    public event UnityAction GameMenuOpened = delegate { };
    public event UnityAction DisplayMenuOpened = delegate { };
    public event UnityAction AudioMenuOpened = delegate { };
    public event UnityAction ControlsMenuOpened = delegate { };

    private void Awake()
    {
        _gameButton = _gameObject.GetComponent<Button>();
        _displayButton = _displayObject.GetComponent<Button>();
        _audioButton = _audioObject.GetComponent<Button>();
        _controlsButton = _controlsObject.GetComponent<Button>();
    }

    private void Start()
    {
        ChangeSelectionColor(_gameButton, _selectionColor);
        ChangeSelectionColor(_displayButton, _selectionColor);
        ChangeSelectionColor(_audioButton, _selectionColor);
        ChangeSelectionColor(_controlsButton, _selectionColor);
        
    }

    private void OnEnable()
    {
        _gameButton.onClick.AddListener(OpenGameMenu);
        _displayButton.onClick.AddListener(OpenDisplayMenu);
        _audioButton.onClick.AddListener(OpenAudioMenu);
        _controlsButton.onClick.AddListener(OpenControlsMenu);
        _gameButton.Select();
    }

    private void OnDisable()
    {
        _gameButton.onClick.RemoveListener(OpenGameMenu);
        _displayButton.onClick.RemoveListener(OpenDisplayMenu);
        _audioButton.onClick.RemoveListener(OpenAudioMenu);
        _controlsButton.onClick.RemoveListener(OpenControlsMenu);
    }

    private void OpenGameMenu()
    {
        GameMenuOpened.Invoke();
    }

    private void OpenDisplayMenu()
    {
        DisplayMenuOpened.Invoke();
    }

    private void OpenAudioMenu()
    {
        AudioMenuOpened.Invoke();
    }

    private void OpenControlsMenu()
    {
        ControlsMenuOpened.Invoke();
    }

    private void ChangeSelectionColor(Selectable button, Color color)
    {
        var block = button.colors;
        block.selectedColor = color;
        button.colors = block;
    }

}
