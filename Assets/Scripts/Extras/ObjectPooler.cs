using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private int poolSize = 10;
    [SerializeField] private bool poolCanExpand = true;

    private List<GameObject> poolObjects;
    private GameObject poolContainer;

    private void Start()
    {
        poolContainer = new GameObject("Pooler: " + objectPrefab.name);
        CreatePool();
    }

    // Creates the pool with all the objects
    private void CreatePool()
    {
        poolObjects = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            AddObjectToPool();
        }
    }

    // Adds one object to the pool
    private GameObject AddObjectToPool()
    {
        GameObject newObject = Instantiate(objectPrefab);
        newObject.SetActive(false);
        newObject.transform.SetParent(poolContainer.transform);

        poolObjects.Add(newObject);
        return newObject;
    }

    // Returns one object from the pool
    public GameObject GetObjectFromPool()
    {
        for (int i = 0; i < poolObjects.Count; i++)
        {
            if (!poolObjects[i].activeInHierarchy)
            {
                return poolObjects[i];
            }
        }

        if (poolCanExpand)
        {
            return AddObjectToPool();
        }

        return null;
    }
}
