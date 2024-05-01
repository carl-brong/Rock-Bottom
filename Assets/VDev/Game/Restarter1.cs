using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Carl Brong modification of Restarter
public class Restarter1 : MonoBehaviour
{
    private GameObject _playerObject;
    private Player _player;
    
    public void RestartGame()
    {
        _playerObject = GameObject.FindWithTag("Player");
        _player = _playerObject.GetComponent<Player>();
        _player.transform.position = _player.startpos;
        _player.Rb.velocity = Vector2.zero;
        _player.HealHealth(1000);
        //_player.transform.position = _player.startpos;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
