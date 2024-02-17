using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchTimelines : MonoBehaviour
{
    [SerializeField] GameObject grid1Parent;
    [SerializeField] GameObject grid1;
    [SerializeField] GameObject grid2Parent;
    [SerializeField] GameObject grid2;
    bool toggle = false;
    // Start is called before the first frame update
    void Start()
    {
        grid1.SetActive(toggle);
        grid2.SetActive(!toggle);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            toggle = !toggle;
            grid1.SetActive(toggle);
            grid2.SetActive(!toggle);
            //SceneManager.LoadScene("Level1");
        }

    }
}
