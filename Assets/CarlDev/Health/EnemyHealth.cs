using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    public float initHealth { get; private set; }
    public float curHealth { get; private set; }
    public GameObject enemy;
    private void Awake()
    {

        initHealth = 1;
        curHealth = initHealth;
    }
    public void takeDamage(float damage)
    {
        curHealth = Mathf.Clamp(curHealth - damage, 0, initHealth);
        //curHelath -= damage;
        if (curHealth > 0)
        {

        }
        else
        {

            Destroy(enemy);
        }
    }

    
}
