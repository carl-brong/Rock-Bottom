using UnityEngine;


public class PersistObject : MonoBehaviour
{
    private static PersistObject _instance;
    
    private void Awake()
    {
        if (_instance is not null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
