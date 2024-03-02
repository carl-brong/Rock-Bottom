
using System.Collections.Generic;
using UnityEngine;

public class MenuController
{
    private static MenuController _instance;

    public static MenuController Instance
    {
        get { return _instance ??= new MenuController(); }
    }

    private Stack<MenuContainer> _menuStack = new Stack<MenuContainer>();

    public void PushMenu(GameObject newMenu, GameObject newFirstSelected)
    {
        var newItem = new MenuContainer(newMenu, newFirstSelected);
        _menuStack.Push(newItem);
        newMenu.SetActive(true);
    }

    public void PopMenu()
    {
        _menuStack.Pop().Menu.SetActive(false);
    }

    public void PopAllMenus()
    {
        var count = _menuStack.Count;
        for (var i = 0; i < count; i++)
        {
            PopMenu();
        }
    }

    public MenuContainer PeekMenu()
    {
        return _menuStack.Peek();
    }

    public bool IsEmpty()
    {
        return _menuStack.Count == 0;
    }

}
