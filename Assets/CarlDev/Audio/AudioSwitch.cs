using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Carl Brong
public class AudioSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioSource track1;
    private void Start()
    {
        
    }

    public void ChangeMusic(AudioClip track2)
    {
        if(track1.clip.name == track2.name)
        {
            return;
        }
        track1.Stop();
        track1.clip = track2;
        track1.Play();
    }
}
