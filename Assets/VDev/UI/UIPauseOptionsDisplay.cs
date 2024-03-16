
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPauseOptionsDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _windowObject;
    [SerializeField] private GameObject _resolutionObject;

    private TMP_Dropdown _windowDropdown;
    private TMP_Dropdown _resolutionDropdown;

    public event UnityAction<int> WindowSelected = delegate { };
    public event UnityAction<int> ResolutionSelected = delegate { };

    private void Awake()
    {
        _windowDropdown = _windowObject.GetComponent<TMP_Dropdown>();
        _resolutionDropdown = _resolutionObject.GetComponent<TMP_Dropdown>();
    }

    private void Start()
    {
        _windowDropdown.value = PlayerPrefs.GetInt("Window", 0);
        _resolutionDropdown.value = PlayerPrefs.GetInt("Resolution", 0);

        if (_windowDropdown.value != 0)
        {
            _resolutionObject.SetActive(false);
        }
        else
        {
            _resolutionObject.SetActive(true);
        }
    }
    
    private void OnEnable()
    {
        _windowDropdown.onValueChanged.AddListener(SelectWindow);
        _resolutionDropdown.onValueChanged.AddListener(SelectResolution);
        _windowDropdown.Select();
    }

    private void OnDisable()
    {
        _windowDropdown.onValueChanged.RemoveListener(SelectWindow);
        _resolutionDropdown.onValueChanged.RemoveListener(SelectResolution);
    }
    
    private void SelectWindow(int arg0)
    {
        WindowSelected.Invoke(arg0);
    }

    private void SelectResolution(int arg0)
    {
        ResolutionSelected.Invoke(arg0);
    }

}
