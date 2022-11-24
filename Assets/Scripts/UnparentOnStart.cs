using UnityEngine;

public class UnparentOnStart : MonoBehaviour
{

    void Start()
    {
        transform.SetParent(null);
    }

}
