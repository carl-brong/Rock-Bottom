using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTimer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float timer = 10.0f;
    [SerializeField] private int sceneNumber;

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            SceneManager.LoadScene(sceneNumber);
        }
    }
}
