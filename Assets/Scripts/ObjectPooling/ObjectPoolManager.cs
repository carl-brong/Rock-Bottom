using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Source: https://www.youtube.com/watch?v=9O7uqbEe-xc

// Vincent Lee
// 5/2/24

public class ObjectPoolManager : MonoBehaviour
{
    private static readonly List<ObjectPool> ObjPools = new List<ObjectPool>();

    public static GameObject SpawnObject(GameObject objPrefab, Vector3 spawnPos, Quaternion spawnRot, Transform folder)
    {
        // Select a specified pool from the list based on name
        var pool = ObjPools.Find(p => p.Name == objPrefab.name);
        
        // If nothing is found, then create a new pool and add it to the list
        if (pool == null)
        {
            pool = new ObjectPool() { Name = objPrefab.name };
            ObjPools.Add(pool);
        }
        
        // Check if there are any inactive objects in pool
        var spawn = pool.ObjPool.FirstOrDefault();

        
        if (spawn == null)
        {
            // If there aren't any then create an instance
            spawn = Instantiate(objPrefab, spawnPos, spawnRot, folder);
        }
        else
        {
            // Otherwise reuse an object from the pool
            spawn.transform.position = spawnPos;
            spawn.transform.rotation = spawnRot;
            pool.ObjPool.Remove(spawn);
            spawn.SetActive(true);
        }

        return spawn;
    }

    public static void ReturnObjectToPool(GameObject obj)
    {
        // Truncate object clone name for lookup
        var name = obj.name[..^7];
        var pool = ObjPools.Find(p => p.Name == name);
        
        // If not null then release object to pool
        if (pool == null) return;
        obj.SetActive(false);
        pool.ObjPool.Add(obj);
        
    }
}

public class ObjectPool
{
    public string Name;
    public List<GameObject> ObjPool = new();
}
