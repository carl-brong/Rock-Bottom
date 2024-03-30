using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowTextUI : MonoBehaviour
{
   
    [SerializeField] GameObject TextUIObject;
    private TextMeshProUGUI fadeAwayText;
    [SerializeField] float waitTime;
    private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    private bool hasPlayed = false; 

    // Start is called before the first frame update
    void Start()
    {
        fadeAwayText = GetComponent<TextMeshProUGUI>(); 
        TextUIObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            audioSource = GetComponent<AudioSource>();
            if (!hasPlayed)
            {
                
                audioSource.PlayOneShot(audioClip);
                hasPlayed = true;
            }
            

            TextUIObject.SetActive(true);
            StartCoroutine("WaitForTime");

            
            

                
    

        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(TextUIObject);
        Destroy(gameObject);
    }
}
