using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiffcultySetter : MonoBehaviour
{
    [SerializeField] private GameObject _triggers;
    [SerializeField] private GameObject _difficultyObject;

    private TMP_Dropdown _difficultyDropdown;
    
    private void Awake()
    {
        _difficultyDropdown = _difficultyObject.GetComponent<TMP_Dropdown>();
        if (PlayerPrefs.GetInt("Difficulty") == 1)
        {
            _triggers.SetActive(false);
        }
        else
        {
            _triggers.SetActive(true);
        }
    }

    public void SetDifficulty()
    {
        if (_difficultyDropdown.value == 1)
        {
            _triggers.SetActive(false);
        }
        else
        {
            _triggers.SetActive(true);
        }

    }
}
