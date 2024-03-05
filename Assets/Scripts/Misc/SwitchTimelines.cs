using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchTimelines : MonoBehaviour
{

    [SerializeField] GameObject Past;
    [SerializeField] GameObject Present;
    [SerializeField] AudioSource SwitchAudio;
    [SerializeField] private InputReader _input;
    bool toggle = false;
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
        toggle = !toggle;
        Past.SetActive(toggle);
        Present.SetActive(!toggle);
        SwitchAudio.Play(0);
    }
}
