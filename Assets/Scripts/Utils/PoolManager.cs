
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Use a separate InputControl component for setting up input.
public abstract class PoolManager : MonoSingleton<PoolManager> {
    public string poolName = "PoolManager";
    public int poolCount = 0;
    public GameObject prefab;

    private List<GameObject> pool;
    private GameObject container;

    private void Start()
    {
        container = new GameObject(poolName);
        pool = GeneratePool(poolCount);
    }

    private List<GameObject> GeneratePool(int count = 0)
    {
        for (int i = 0; i < count; i++) {
            GameObject go = Instantiate(prefab);
            go.transform.parent = container.transform;
            go.SetActive(false);
            pool.Add(go);
        }
        return pool;
    }

    public GameObject GetChild()
    {
        foreach (var child in pool) {
            if (!child.activeInHierarchy) {
                child.SetActive(true);
                return child;
            }
        }

        GameObject newChild = GeneratePool(1).Last();
        newChild.SetActive(true);

        return newChild;
    }

}
