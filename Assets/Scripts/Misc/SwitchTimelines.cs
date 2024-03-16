using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*Carl Brong
  SwitchTimelines
*/
public class SwitchTimelines : MonoBehaviour
{

    [SerializeField] GameObject Past;
    [SerializeField] GameObject Present;
    [SerializeField] AudioSource SwitchAudio;
    [SerializeField] private InputReader _input;
    bool toggle = false;
    bool canSwitch = true;
    
    // Start is called before the first frame update
    void Start()
    {
        Past.SetActive(toggle);
        Present.SetActive(!toggle);
        SwitchAudio = GetComponent<AudioSource>();
        _input.SwitchEvent += Swap;
        
    }

    private void OnDestroy()
    {
        _input.SwitchEvent -= Swap;
    }
    
    private void Swap()
    {
        if (!canSwitch) return;
        toggle = !toggle;
        Past.SetActive(toggle);
        Present.SetActive(!toggle);
        SwitchAudio.Play(0);
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
