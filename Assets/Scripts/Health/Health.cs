using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float initHealth { get; private set; }
    //get in another script but only through here
    public float curHealth { get; private set; }
    private Animator anim;
    AudioSource audioData;
    private void Awake()
    {
        initHealth = 1;
        curHealth = initHealth;
        anim = GetComponent<Animator>();
    }
    public void takeDamage(float damage)
    {
        curHealth = Mathf.Clamp(curHealth - damage, 0, initHealth);
        //curHelath -= damage;
        if (curHealth > 0)
        {
            anim.SetTrigger("hurt");
        }
        else
        {
            anim.SetTrigger("death");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
