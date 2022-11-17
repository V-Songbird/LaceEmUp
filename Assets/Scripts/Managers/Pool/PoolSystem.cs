using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSystem : MonoBehaviour
{
    [SerializeField] List<PoolObject> ObjectsInPool = new List<PoolObject>();

    private Dictionary<string, Queue<GameObject>> _pooledObjectDictionary;

    public Transform ObjectPoolParent => transform;

    private bool _objectPoolHasBeenGenerated;


    public bool ObjectPoolHasBeenGenerated => _objectPoolHasBeenGenerated;

    private void Awake()
    {
        _pooledObjectDictionary = new Dictionary<string, Queue<GameObject>>();
        GenerateObjectPool();
    }

    public void GenerateObjectPool()
    {
        if (_objectPoolHasBeenGenerated) return;

        foreach (var pooledObject in ObjectsInPool)
        {
            var objectPool = new Queue<GameObject>();
            if (ReferenceEquals(pooledObject.prefab, null)) continue;

            for (var i = 0; i < pooledObject.count; i++)
            {
                var obj = Instantiate(pooledObject.prefab, ObjectPoolParent, true);
                obj.name += " " + i;
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            _pooledObjectDictionary.Add(pooledObject.identifier, objectPool);
        }

        _objectPoolHasBeenGenerated = true;
    }


    public void ClearObjectPool()
    {
        foreach (var pooledObject in _pooledObjectDictionary)
        {
            foreach (var queuedObject in pooledObject.Value)
            {
                Destroy(queuedObject);
            }
        }
        _pooledObjectDictionary.Clear();
        _objectPoolHasBeenGenerated = false;
    }


    public GameObject SpawnFromPool(string identifier, Vector3 position)
    {
        return SpawnFromPool(identifier, position, Quaternion.identity, Vector3.one, ObjectPoolParent);
    }

    public GameObject SpawnFromPool(string identifier, Vector3 position, Quaternion rotation)
    {
        return SpawnFromPool(identifier, position, rotation, Vector3.one, ObjectPoolParent);
    }

    public GameObject SpawnFromPool(string identifier, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        return SpawnFromPool(identifier, position, rotation, scale, ObjectPoolParent);
    }

    public GameObject SpawnFromPool(string identifier, Vector3 position, Quaternion rotation, Vector3 scale, Transform parent)
    {
        if (string.IsNullOrEmpty(identifier)) return null;

        if (!_pooledObjectDictionary.ContainsKey(identifier))
        {
            Debug.LogWarning("OBJECT POOL WARNING: Attempted to spawn a pooled object with an unknown identifier (" + identifier + ") at " + Time.time);
            return null;
        }

        var objectToSpawn = _pooledObjectDictionary[identifier].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.transform.localScale = scale;
        objectToSpawn.transform.parent = parent;

        _pooledObjectDictionary[identifier].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
