using UnityEngine;

// Vincent Lee
// 5/2/24

public class DifficultyInit : MonoBehaviour
{
    private void Awake()
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive((PlayerPrefs.GetInt("Difficulty", 0) != 1));
        }
    }
}
