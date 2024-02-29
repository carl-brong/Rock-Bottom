using Cinemachine;
using UnityEngine;


public class CameraBlending : MonoBehaviour
{
    private CinemachineBrain _brain;

    private void Awake()
    {
        _brain = GetComponent<CinemachineBrain>();
    }

    private void Update()
    {
        if (_brain.IsBlending)
        {
            Time.timeScale = 0;
        }
        else
        {
            if (GameManager.Instance.CurrentGameState == GameState.Gameplay) 
                Time.timeScale = 1;
        }
    }
}
