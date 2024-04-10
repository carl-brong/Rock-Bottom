using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restarter : MonoBehaviour
{
    private GameObject _playerObject;
    private Player _player;
    public bool isRestarted = false;
    
    public void RestartGame()
    {
        _playerObject = GameObject.FindWithTag("Player");
        _player = _playerObject.GetComponent<Player>();
        _player.transform.position = _player.startpos;
        _player.Rb.velocity = Vector2.zero;
        _player.HealHealth(1000);
        _player.transform.position = _player.startpos;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        isRestarted = true;

    }
}
