using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowTextUI : MonoBehaviour
{
    [SerializeField] float fadeTime;
    [SerializeField] GameObject TextUIObject;
    private TextMeshProUGUI fadeAwayText;
    [SerializeField] float waitTime;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        fadeAwayText = GetComponent<TextMeshProUGUI>(); 
        TextUIObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            
            TextUIObject.SetActive(true);
            StartCoroutine("WaitForTime");
            audioSource = GetComponent<AudioSource>();
            audioSource.Play(0);


        }
    }

    // Update is called once per frame
    void Update()
    {

        /*if (fadeTime < 0)
        {
            fadeTime += Time.deltaTime;
            fadeAwayText.color = new Color(fadeAwayText.color.r, fadeAwayText.color.g, fadeAwayText.color.b, fadeTime);
        }*/

        if (fadeTime > 0)
        {
            fadeTime -= Time.deltaTime;
            fadeAwayText.color = new Color(fadeAwayText.color.r, fadeAwayText.color.g, fadeAwayText.color.b, fadeTime);
        }
    }
    IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(TextUIObject);
        Destroy(gameObject);
    }
}
