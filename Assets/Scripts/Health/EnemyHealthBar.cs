using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyHealthBar: MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private EnemyHealth enemyHealth;
    [SerializeField] private UnityEngine.UI.Image totalBar;
    [SerializeField] private UnityEngine.UI.Image currentBar;

    private void Start()
    {

        totalBar.fillAmount = 1;
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        if (enemy != null)
        {
            enemyHealth = enemy.GetComponent<EnemyHealth>();
        }
    }

    private void Update()
    {
        currentBar.fillAmount = enemyHealth.curHealth;
        transform.rotation = Camera.main.transform.rotation;
        transform.position = target.position + offset;
    }

}
