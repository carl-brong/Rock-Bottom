using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private Transform firePosition;
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform folder;
    
    
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(2);
        ObjectPoolManager.SpawnObject(prefab, firePosition.position, firePosition.rotation, folder);

        StartCoroutine(Shoot());
    }
}
