using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    
    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
    }
    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log("Collision detected with " + collision.gameObject.name);
        if (collision.gameObject.tag == "Player")
        {
  
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level1"))
            {
                SceneManager.LoadScene(2);
            }
            else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level2"))
            {
                SceneManager.LoadScene(3);
            }
            else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level3"))
            {
                SceneManager.LoadScene(4);
            }
            else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Cutscene"))
            {
                SceneManager.LoadScene(5);
            }
            else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level4"))
            {
                SceneManager.LoadScene(6);
            }

        }
        
    }

    
     
}
