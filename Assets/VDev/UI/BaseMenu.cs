using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Vincent Lee
// 4/25/24

public abstract class BaseMenu : MonoBehaviour
{
    public InputReader inputReader;
    public bool topLevel;
    public Canvas prevMenu;
    private Selectable firstSelected;

    public virtual void Awake()
    {
        firstSelected = GetComponentInChildren<Selectable>();
    }
    
    private IEnumerator DelaySelect()
    {
        yield return new WaitForEndOfFrame();
        firstSelected.Select();
    }
    
    public virtual void OnEnable()
    {
        inputReader.EnableMenuControls();
        StartCoroutine(DelaySelect());
        
        if (!topLevel)
            inputReader.PopMenuEvent += Return;
    }

    public virtual void OnDisable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        if (!topLevel)
            inputReader.PopMenuEvent -= Return;
    }

    private void Return()
    {
        if (prevMenu == null) return;
        gameObject.SetActive(false);
        prevMenu.gameObject.SetActive(true);
    }
}

