using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToTitle : MonoBehaviour
{
    private void TearDown()
    {
        var objs = FindObjectsOfType<PersistObject>(true);
        foreach (var obj in objs)
        {
            Destroy(obj.gameObject);
        }

    }

    public void ExitToTitle()
    {
        TearDown();
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }
}
