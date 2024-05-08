using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Carl Brong
public class AIFollow : MonoBehaviour
{
    private GameObject player;
    public float speed;
    private float distance;
    [SerializeField] private int minDistance = 30;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        Vector3 scale = transform.localScale;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //moves at certain speed
        
        if(player.transform.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        else
        {
            scale.x = Mathf.Abs(scale.x) * -1;
        }
        if(distance < minDistance)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            //transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }

        transform.localScale = scale;
    }
}
