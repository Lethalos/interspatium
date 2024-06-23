using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public int attackMode = 0;
    public GameObject objectPrefab1; // The prefab for the pooled objects
    public GameObject objectPrefab2; // The prefab for the pooled objects
    public GameObject objectPrefab3; // The prefab for the pooled objects
    public GameObject objectPrefab4;
    private GameObject objectPrefab;

    public int poolSize = 10; // The initial size of the pool

    private Queue<GameObject> pool = new Queue<GameObject>();

    void Start()
    {
        switch (attackMode)
        {
            case 1:
                objectPrefab = objectPrefab1;
                break;
            case 2:
                objectPrefab = objectPrefab2;
                break;
            case 3:
                objectPrefab = objectPrefab3;
                break;
            case 4:
                objectPrefab = objectPrefab4;
                break;
            default:
                objectPrefab = objectPrefab1;
                break;
        }
        // Initialize the pool with inactive objects
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objectPrefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetObject()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            // If the pool is empty, create a new object
            GameObject obj = Instantiate(objectPrefab);
            obj.SetActive(true);
            return obj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}