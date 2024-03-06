using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*Carl Brong
  SwitchTimelines
*/
public class SwitchTimelines : MonoBehaviour
{

    public GameObject Past;
    public GameObject Present;
    private AudioSource SwitchAudio;
    bool toggle = false;
    bool canSwitch = true;
    // Start is called before the first frame update
    void Start()
    {
        
        Past.SetActive(toggle);
        Present.SetActive(!toggle);
        SwitchAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        switchTime();
    }

    public void switchTime()
    {
        if (Input.GetKeyDown(KeyCode.Return) && canSwitch)

        {
            toggle = !toggle;
            Past.SetActive(toggle);
            Present.SetActive(!toggle);
            SwitchAudio.Play(0);

        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            canSwitch = false;
            Debug.Log("Tile overlap");
        }
        
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            canSwitch = true;
            Debug.Log("Out of tile");
        }
        
    }
}
        



