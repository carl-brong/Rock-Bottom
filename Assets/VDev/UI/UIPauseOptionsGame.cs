
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class UIPauseOptionsGame : MonoBehaviour
{
    [SerializeField] private GameObject _difficultyObject;

    private TMP_Dropdown _difficultyDropdown;

    public event UnityAction<int> DifficultySelected = delegate { };

    private void Awake()
    {
        _difficultyDropdown = _difficultyObject.GetComponent<TMP_Dropdown>();
    }

    private void Start()
    {
        _difficultyDropdown.value = PlayerPrefs.GetInt("Difficulty", 0);
    }

    private void OnEnable()
    {
        _difficultyDropdown.onValueChanged.AddListener(SelectDifficulty);
        _difficultyDropdown.Select();
    }


    private void OnDisable()
    {
        _difficultyDropdown.onValueChanged.RemoveListener(SelectDifficulty);
    }
    
    private void SelectDifficulty(int arg0)
    {
        DifficultySelected.Invoke(arg0);
    }

}
