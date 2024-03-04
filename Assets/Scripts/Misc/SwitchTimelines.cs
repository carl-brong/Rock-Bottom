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
        if (Input.GetKeyDown(KeyCode.Return))

        {
            toggle = !toggle;
            Past.SetActive(toggle);
            Present.SetActive(!toggle);
            SwitchAudio.Play(0);

        }

    }
}
        



