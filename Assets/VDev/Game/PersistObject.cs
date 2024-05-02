using UnityEngine;

// Vincent Lee
// 5/1/24

public class PersistObject : MonoBehaviour
{
    private string objectID;
    
    private void Awake()
    {
        objectID = name;
    }

    private void Start()
    {
        var objects = FindObjectsOfType<PersistObject>();
        foreach (var obj in objects)
        {
            if (obj == this) break;
            if (obj.objectID == objectID)
            {
                Debug.Log("Destroyed " + gameObject.name);
                Destroy(gameObject);
            }
        }

        if (gameObject.transform.parent != null) gameObject.transform.parent = null;
        DontDestroyOnLoad(gameObject);
    }
}
