using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    [SerializeField]
    private Pool[] pools;

    private readonly Dictionary<string, Stack<GameObject>> _pool = new();

    private void Start()
    {
        if (pools != null)
        {
            for (int i = 0; i < pools.Length; i++)
            {
                SpawnObject(pools[i].Prefab, pools[i].Count);
            }
        }
    }

    private void SpawnObject(GameObject prefab, int count)
    {
        Debug.Log("생성!");
        var key = $"{prefab.name.Replace("(Clone)", "")}";
        _pool.Add(key, new Stack<GameObject>());

        for (int i = 0; i < count; i++)
        {
            GameObject spawnObject = Instantiate(prefab);
            spawnObject.name = key;
            spawnObject.SetActive(false);
            spawnObject.transform.SetParent(transform);
            _pool[prefab.name].Push(spawnObject);
        }
    }

    public GameObject SpawnObject(GameObject prefab, Vector3 position = default, Quaternion rotation = default, Transform parent = null)
    {
        Debug.Log("스폰!");
        var key = $"{prefab.name.Replace("(Clone)", "")}";

        if (!_pool.ContainsKey(key))
            _pool.Add(key, new Stack<GameObject>());

        if (parent == null)
            parent = transform;

        GameObject spawnObject;

        if (_pool[key].Count > 0)
        {
            spawnObject = _pool[key].Pop();
            spawnObject.SetActive(true);
        }
        else
        {
            spawnObject = Instantiate(prefab);
            spawnObject.name = key;
        }

        spawnObject.transform.SetParent(parent);

        if (position != default)
            spawnObject.transform.position = position;

        if (rotation != default)
            spawnObject.transform.rotation = rotation;
        else
            spawnObject.transform.rotation = Quaternion.identity;

        return spawnObject;
    }

    public T SpawnObject<T>(T prefab, Vector3 position = default, Quaternion rotation = default, Transform parent = null) where T : Component
    {
        return SpawnObject(prefab.gameObject, position, rotation, parent).GetComponent<T>();
    }

    public void DestroyObject(GameObject prefab)
    {
        prefab.SetActive(false);
        _pool[prefab.name].Push(prefab);
    }
}

[System.Serializable]
public class Pool
{
    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private int count;
    public GameObject Prefab => prefab;
    public int Count => count;
}

