
using UnityEngine;

public class MenuContainer
{
    public GameObject Menu { get; private set; }
    public GameObject FirstSelected { get; private set; }
    
    public MenuContainer(GameObject menu, GameObject firstSelected)
    {
        Menu = menu;
        FirstSelected = firstSelected;
    }
}
