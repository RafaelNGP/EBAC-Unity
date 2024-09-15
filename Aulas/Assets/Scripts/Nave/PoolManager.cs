using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] List<GameObject> poolObjects = new();
    public int amount = 20;

    private void Awake()
    {
        StartPool();
    }

    private void StartPool()
    {
        for (int i = 0; i < amount; i++)
        {
            var obj = Instantiate(prefab, transform);
            obj.SetActive(false);
            poolObjects.Add(obj);
        }
    }

    public GameObject GetPoolGObjects()
    {
        for (int i = 0; i < amount; i++)
        {
            if (!poolObjects[i].activeInHierarchy)
            {
                return poolObjects[i];
            }
        }
        return null;
    }
}
