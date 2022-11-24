using UnityEngine;


namespace LaceEmUp.Helpers
{
    public class Rotate : MonoBehaviour
    {

        [SerializeField] private float speed;

        private void Update()
        {
            transform.Rotate(Vector3.forward, speed, Space.Self);
        }

    }
}
