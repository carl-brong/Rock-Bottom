using UnityEngine;
using UnityEngine.EventSystems;


public class OptionsScript : MonoBehaviour
{
    [SerializeField] private GameObject firstSelected;
    [SerializeField] private EventSystem eventSystem;
    
    
    public void OnClick(GameObject previousPage)
    {
        previousPage.SetActive(false);
        gameObject.SetActive(true);
        eventSystem.SetSelectedGameObject(firstSelected);
        
    }    
}
