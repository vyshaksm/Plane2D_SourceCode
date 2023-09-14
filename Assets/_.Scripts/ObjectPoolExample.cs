using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolExample : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectPrefabs;
    [SerializeField] private int objectCount;
    [SerializeField] private List<Transform> objectParents;

    private List<List<GameObject>> objectPools;

    public static ObjectPoolExample instance;

    private void Start()
    {
        instance = this;
        InitializeObjectPools();
    }

    private void InitializeObjectPools()
    {
        objectPools = new List<List<GameObject>>();

        for (int i = 0; i < objectPrefabs.Count; i++)
        {
            List<GameObject> objectPool = new List<GameObject>();
            for (int j = 0; j < objectCount; j++)
            {
                GameObject obj = Instantiate(objectPrefabs[i], objectParents[i]);
                obj.SetActive(false);
                objectPool.Add(obj);
            }
            objectPools.Add(objectPool);
        }
    }

    public  GameObject GetObject(int objectType)
    {
        //if (instance == null || instance.objectPools == null || objectType < 0 || objectType >= instance.objectPools.Count)
        //{
        //    Debug.LogError("Object pool or object type not found.");
        //    return null;
        //}

        List<GameObject> objectPool = instance.objectPools[objectType];

        for (int i = 0; i < objectPool.Count; i++)
        {
            if (!objectPool[i].activeInHierarchy)
            {
                //objectPool[i].SetActive(true);
                return objectPool[i];
            }
        }

        //// If all objects in the pool are in use, create a new one and add it to the pool
        //GameObject newObj = Instantiate(instance.objectPrefabs[objectType], instance.objectParents[objectType]);
        //newObj.SetActive(false);
        //objectPool.Add(newObj);
        //return newObj;
        return null;
    }
}
