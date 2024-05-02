using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBandaid : MonoBehaviour
{
    // Start is called before the first frame
    [SerializeField] GameObject MenuItem;
    private bool toggle = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            toggle = !toggle;
            Debug.Log("Pressed");
            MenuItem.SetActive(toggle);
        }
    }
}
