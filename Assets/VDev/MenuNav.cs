using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuNav : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private GameObject _nextMenu;
    [SerializeField] private GameObject _firstSelected;
    [SerializeField] private bool _debug;

    public void Clicked()
    {
        MenuController.Instance.PushMenu(_nextMenu, _firstSelected);
        Log($"Pushed {_nextMenu}");
        EventSystem.current.SetSelectedGameObject(_firstSelected);
    }
    
    private void Log(string message)
    {
        if (_debug) Debug.Log(message);
    }
}
