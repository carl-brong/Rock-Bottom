using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    float rbX, rbY;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        rbX = rb.position.x;
        rbY = rb.position.y;
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SceneManager.LoadScene("SurfaceChild");
            rb.position = new Vector3(rbX, rbY);
        }
    }
}
