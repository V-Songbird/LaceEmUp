using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterColliders : MonoBehaviour
{
    [SerializeField] List<GameObject> waterColliders = new List<GameObject>();


    public void ActiveWaterColliders()
    {
        foreach (GameObject go in waterColliders)
        {
            go.SetActive(true);
        }
    }

    public void DesactiveWaterColliders()
    {
        foreach (GameObject go in waterColliders)
        {
            go.SetActive(false);
        }
    }
}
