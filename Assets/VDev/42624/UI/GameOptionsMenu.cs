
using TMPro;
using UnityEngine;

// Vincent Lee
// 4/26/24

public class GameOptionsMenu : BaseMenu
{
    public TMP_Dropdown difficultyButton;

    public override void Awake()
    {
        base.Awake();
        difficultyButton.value = PlayerPrefs.GetInt("Difficulty", 0);
        difficultyButton.onValueChanged.AddListener(x => PlayerPrefs.SetInt("Difficulty", x));
    }
}
