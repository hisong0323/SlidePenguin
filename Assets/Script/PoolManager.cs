using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    private readonly Dictionary<string, Stack<GameObject>> _pool = new();

    public GameObject SpawnObject(GameObject prefab, Vector3 position = default, Quaternion rotation = default, Transform parent = null)
    {
        var key = $"{prefab.name.Replace("(Clone)", "")}";

        if (!_pool.ContainsKey(key))
            _pool.Add(key, new Stack<GameObject>());

        if (parent == null)
            parent = transform;

        GameObject spawnObject;

        if (_pool[key].Count > 0)
        {
            spawnObject = _pool[key].Pop();
            spawnObject.transform.SetParent(parent);
            spawnObject.SetActive(true);
        }
        else
        {
            spawnObject = Instantiate(prefab, parent);
            spawnObject.name = key;
        }

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
