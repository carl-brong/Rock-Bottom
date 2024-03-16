using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Carl Brong MovablePlatforms Script

public class MovablePlatform : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float speed;
    [SerializeField] int startPos;
    [SerializeField] Transform[] boundPoints;
    private int i;
    void Start()
    {
        transform.position = boundPoints[startPos].position;
    }

    // Update is called once per frame
    void Update()
    {
        //check distance of platform and bound points
        if (Vector2.Distance(transform.position, boundPoints[i].position) < 0.02f) {
            i++;
            if(i == boundPoints.Length) //check if platform was at the last point after the index increase
            {
                i = 0; //reset index
            }
        }
        //move the platform
        transform.position = Vector2.MoveTowards(transform.position, boundPoints[i].position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(transform.position.y < collision.transform.position.y -1.0f)
            {
                collision.transform.SetParent(transform);
            }
            
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }
}
