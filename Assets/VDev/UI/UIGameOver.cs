
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] private GameObject _restartObject, _exitObject;
    
    
    private Button _restartButton, _exitButton;
    
    
    public event UnityAction<int> RestartedGame = delegate { };
    public event UnityAction<int> ExitLevelRequested = delegate { };

    private void Awake()
    {
        _restartButton = _restartObject.GetComponent<Button>();
        _exitButton = _exitObject.GetComponent<Button>();
    }

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(Restart);
        _exitButton.onClick.AddListener(ExitLevel);
        StartCoroutine(Delay());
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(Restart);
        _exitButton.onClick.RemoveListener(ExitLevel);
    }

    private void Restart()
    {
        RestartedGame.Invoke(0);
    }

    private void ExitLevel()
    {
        ExitLevelRequested.Invoke(1);
    }
    

    private IEnumerator Delay()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        _restartButton.Select();
    }
}
