
using System;
using TMPro;
using UnityEngine;

// Vincent Lee
// 4/26/24

public class GameOptionsMenu : BaseMenu
{
    public TMP_Dropdown difficultyButton;
    public TMP_Text normDesc, hardDesc;

    public override void Awake()
    {
        base.Awake();
        difficultyButton.value = PlayerPrefs.GetInt("Difficulty", 0);
        
        
        
        difficultyButton.onValueChanged.AddListener(x =>
        {
            Debug.Log(x);
            PlayerPrefs.SetInt("Difficulty", x);
            GameSingleton.Instance.SetDifficulty();
        });
    }

    public void Update()
    {
        if (difficultyButton.value == 0)
        {
            normDesc.gameObject.SetActive(true);
            hardDesc.gameObject.SetActive(false);
        }
        else
        {
            normDesc.gameObject.SetActive(false);
            hardDesc.gameObject.SetActive(true);
        }
    }
}
