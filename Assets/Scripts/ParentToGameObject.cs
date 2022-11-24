using UnityEngine;

public class ParentToGameObject : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private bool worldPositionStays;
    [SerializeField] private bool moveToPosition;

    private void Awake()
    {
        if (moveToPosition)
        {
            transform.position = parent.transform.position;
        }
        
        transform.SetParent(parent, worldPositionStays);
    }

}
