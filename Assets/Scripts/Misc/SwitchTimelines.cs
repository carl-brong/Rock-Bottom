using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchTimelines : MonoBehaviour
{

    [SerializeField] GameObject Past;
    [SerializeField] GameObject Present;
    [SerializeField] AudioSource SwitchAudio;
    bool toggle = false;
    // Start is called before the first frame update
    void Start()
    {
        Past.SetActive(toggle);
        Present.SetActive(!toggle);
        SwitchAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void OnMouseDown()
    {
        
    }
    void Update()
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
