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
   // [SerializeField] GameObject positionAnchor;
    private Player player;
    private GameObject playerObject;
    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        playerObject = GameObject.FindWithTag("Player");
        player = playerObject.GetComponent<Player>();
    }
    private void Update()
    {
       
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

                case 2:
                    
                    Destroy(destroyAudio);
                    Destroy(destroyCamera);
                    
                    SceneManager.LoadScene(3);
                    //player.transform.position = positionAnchor.transform.position;
                    //Destroy(destroyPlayer);
                    //DontDestroyOnLoad(player);


                    break;
                case 3:
                   
                    Destroy(destroyAudio);
                    Destroy(destroyCamera);
                    
                    SceneManager.LoadScene(4);
                    Destroy(destroyPlayer);
                    //player.transform.position = positionAnchor.transform.position;
                    break;
                case 4:
                    
                    Destroy(destroyAudio);
                    Destroy(destroyCamera);
                    Destroy(destroyPlayer);
                    SceneManager.LoadScene(5);
                    //player.transform.position = positionAnchor.transform.position;
                    break;
                case 5:
                    
                    Destroy(destroyAudio);
                    Destroy(destroyCamera);
                    Destroy(destroyPlayer);
                    SceneManager.LoadScene(6);
                    //player.transform.position = positionAnchor.transform.position;
                    break;
                case 6:
                    
                    Destroy(destroyAudio);
                    Destroy(destroyCamera);
                    Destroy(destroyPlayer);
                    SceneManager.LoadScene(7);
                    //player.transform.position = positionAnchor.transform.position;
                    break;
                case 7:
                    
                    Destroy(destroyAudio);
                    Destroy(destroyCamera);
                    Destroy(destroyPlayer);
                    SceneManager.LoadScene(8);
                    //player.transform.position = positionAnchor.transform.position;
                    break;
                case 8:
                    
                    Destroy(destroyAudio);
                    Destroy(destroyCamera);
                    Destroy(destroyPlayer);
                    SceneManager.LoadScene(9);
                    //player.transform.position = positionAnchor.transform.position;
                    break;
                case 9:
                    Destroy(destroyPlayer);
                    Destroy(destroyAudio);
                    Destroy(destroyCamera);
                    SceneManager.LoadScene(1);
                    break;

            }
  
           

        }
        
    }

    /*public void fadeToLevel(int levelIndex)
    {
        animator.SetTrigger("FadeOut");
    }*/

    
     
}
