using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//Carl Brong
public class AudioSwitchTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioClip NewTrack;
    private AudioSwitch audioManager;
    void Start()
    {
        audioManager = FindObjectOfType<AudioSwitch>();
        //NewTrack = GetComponent<AudioClip>();
        //NewAmbience = GetComponent<AudioClip>();
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player"){
            if(NewTrack != null)
            {
                audioManager.ChangeMusic(NewTrack);

            }
            
        }
    }
}
