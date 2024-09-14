using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManagerEnemies : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] List<GameObject> listPoolObjects;
    [SerializeField] int amount = 20;

    private void Start()
    {
        listPoolObjects = new List<GameObject>();
        for (int i = 0; i < amount; i++)
        {
            listPoolObjects.Add(GameObject.Instantiate(prefab));
            listPoolObjects[i].SetActive(false);
        }
    }

    public GameObject GetPoolGObjects()
    {
        foreach (GameObject obj in listPoolObjects) {
            if (obj != null && !obj.activeInHierarchy)
            {
                return obj;
            }
        }
        return null;
    }
}
