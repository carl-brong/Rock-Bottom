using UnityEngine;

public class Switcher : MonoBehaviour
{
    [SerializeField] private GameObject screenFilter;
    private bool filterOn = false;

    private void Start()
    {
        screenFilter.SetActive(filterOn);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            screenFilter.SetActive(!filterOn);
            filterOn = !filterOn;
        }
    }
}
