using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GenericObjectPool<T> where T : MonoBehaviour
{
    [SerializeField] private T prefab;
    [SerializeField] private int poolSize = 50;
    [SerializeField] private Transform parent;
    private List<T> pool;

    public void InitializePool()
    {
        pool = new List<T>();
        for (int i = 0; i < poolSize; i++)
        {
            T obj = GameObject.Instantiate(prefab, parent);
            obj.gameObject.SetActive(false);
            pool.Add(obj);
        }
    }

    public T GetPooledObject()
    {
        foreach (T obj in pool)
        {
            if (!obj.gameObject.activeInHierarchy)
            {
                return obj;
            }
        }
        // Optionally, expand the pool size if needed
        T newObj = GameObject.Instantiate(prefab, parent);
        newObj.gameObject.SetActive(false);
        pool.Add(newObj);
        return newObj;
    }

    public void ReturnToPool(T obj)
    {
        obj.gameObject.SetActive(false);
    }
}
