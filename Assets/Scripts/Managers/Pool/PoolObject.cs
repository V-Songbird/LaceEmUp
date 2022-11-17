
using UnityEngine;

[System.Serializable]
public class PoolObject
{
    public string identifier;
    public GameObject prefab;
    [Range(1, 50)] public int count;
}
