using UnityEngine;


public class PersistObject : MonoBehaviour
{
    private string objectID;
    
    private void Awake()
    {
        objectID = name + gameObject.transform.position.ToString();
    }

    private void Start()
    {
         
        var objects = FindObjectsOfType<PersistObject>();
        foreach (var obj in objects)
        {
            if (obj == this) break;
            if (obj.objectID == objectID) Destroy(gameObject);
  
        }
        
        DontDestroyOnLoad(gameObject);
    }
}
