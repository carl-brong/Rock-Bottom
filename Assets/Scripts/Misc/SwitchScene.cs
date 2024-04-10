using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Carl Brong
public class SwitchScene : MonoBehaviour
{
    public Animator animator;
    [SerializeField] GameObject destroyPlayer;
    [SerializeField] GameObject destroyAudio;
    [SerializeField] GameObject destroyCamera;
    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
    }
    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            fadeToLevel(1);
        }
    }
    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        int levelNumber = SceneManager.GetActiveScene().buildIndex;

        Debug.Log("Collision detected with " + collision.gameObject.name);
        if (collision.gameObject.tag == "Player")
        {
            switch (levelNumber)
            {

                case 1:
                    Destroy(destroyPlayer);
                    Destroy(destroyAudio);
                    Destroy(destroyCamera);
                    SceneManager.LoadScene(2);
                    break;
                case 2:
                    Destroy(destroyPlayer);
                    Destroy(destroyAudio);
                    Destroy(destroyCamera);
                    SceneManager.LoadScene(3); 
                    break;
                case 3:
                    Destroy(destroyPlayer);
                    Destroy(destroyAudio);
                    Destroy(destroyCamera);
                    SceneManager.LoadScene(4);
                    break;
                case 4:
                    Destroy(destroyPlayer);
                    Destroy(destroyAudio);
                    Destroy(destroyCamera);
                    SceneManager.LoadScene(5);
                    break;
                case 5:
                    Destroy(destroyPlayer);
                    Destroy(destroyAudio);
                    Destroy(destroyCamera);
                    SceneManager.LoadScene(6);
                    break;
                case 6:
                    Destroy(destroyPlayer);
                    Destroy(destroyAudio);
                    Destroy(destroyCamera);
                    SceneManager.LoadScene(7);
                    break;
                case 7:
                    Destroy(destroyPlayer);
                    Destroy(destroyAudio);
                    Destroy(destroyCamera);
                    SceneManager.LoadScene(8);
                    break;
                case 8:
                    Destroy(destroyPlayer);
                    Destroy(destroyAudio);
                    Destroy(destroyCamera);
                    SceneManager.LoadScene(0);
                    break;

            }
  
           

        }
        
    }

    public void fadeToLevel(int levelIndex)
    {
        animator.SetTrigger("FadeOut");
    }

    
     
}
