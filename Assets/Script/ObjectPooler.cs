using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler<T> where T : Component
{
#region Singleton
    private static ObjectPooler<T> instance;
    public static ObjectPooler<T> Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ObjectPooler<T>();
                instance.Initialize();
            }
            return instance;
        }
    }
#endregion

    public T prefab;
    public int poolSize = 100;

    private Dictionary<T, List<T>> objectPool;

    private ObjectPooler()
    {
        // Private constructor to enforce singleton pattern
    }

    private void Initialize()
    {
        // Initialize the object pool
        objectPool = new Dictionary<T, List<T>>();
    }

    public void CreatePool(T prefab, int size)
    {
        // Create a new pool for the given prefab
        if (!objectPool.ContainsKey(prefab))
        {
            objectPool[prefab] = new List<T>();
            for (int i = 0; i < size; i++)
            {
                T obj = GameObject.Instantiate(prefab);
                obj.gameObject.SetActive(false);
                objectPool[prefab].Add(obj);
            }
        }
    }

    public T GetFromPool(T prefab)
    {
        // Find an inactive object in the pool and return it
        if (objectPool.ContainsKey(prefab))
        {
            List<T> pool = objectPool[prefab];
            for (int i = 0; i < pool.Count; i++)
            {
                if (!pool[i].gameObject.activeInHierarchy)
                {
                    return pool[i];
                }
            }

            // If no inactive object is found, create a new one and add it to the pool
            T newObj = GameObject.Instantiate(prefab);
            pool.Add(newObj);
            return newObj;
        }

        Debug.LogWarning("Object pool for the given prefab does not exist!");
        return null;
    }

}
