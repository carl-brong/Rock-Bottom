using UnityEngine;
using UnityEngine.SceneManagement;
//Carl Brong
public class TempEnd : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(1);
        Debug.Log("enter");
    }
}
