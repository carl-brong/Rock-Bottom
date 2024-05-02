using System.Collections;
using UnityEngine;

// Vincent Lee
// 5/2/24

public class Cannon : MonoBehaviour
{
    [SerializeField] private Transform firePosition;
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform folder;
    [SerializeField] private float _intervalOffset;
    [SerializeField] int waitTime;
    [SerializeField] char direction;
    
    
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(WaitOffset(_intervalOffset));
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(waitTime);
        var obj = ObjectPoolManager.SpawnObject(prefab, firePosition.position, firePosition.rotation, folder);
        obj.GetComponent<TempProj>().dir = new Vector2(transform.localScale.x, 0);
        StartCoroutine(Shoot());
    }

    private IEnumerator WaitOffset(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        StartCoroutine(Shoot());
    }
}
