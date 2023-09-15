using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectPrefabs;
    [SerializeField] private int objectCount;
    [SerializeField] private List<Transform> objectParents;

    private List<List<GameObject>> objectPools;

    public static ObjectPooling instance;

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

        List<GameObject> objectPool = instance.objectPools[objectType];

        for (int i = 0; i < objectPool.Count; i++)
        {
            if (!objectPool[i].activeInHierarchy)
            {
                return objectPool[i];
            }
        }
        return null;
    }
}
