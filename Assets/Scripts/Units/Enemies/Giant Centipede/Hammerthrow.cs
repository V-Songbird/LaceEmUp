using BehaviorDesigner.Runtime.Tasks.Unity.Math;
using UnityEditor.Rendering;
using UnityEngine;

namespace LaceEmUp.Units
{
    public class Hammerthrow : MonoBehaviour
    {
        [Header("Hammerthrow")]
        [SerializeField] private float targetScanRadius;
        [SerializeField] private float lifeTime;
        [SerializeField] private float speed;
        [SerializeField] private float minDamage;
        [SerializeField] private float maxDamage;
        [SerializeField] private Vector2 knockbackForce;

        private Vector2 direction;
        private float timeAlive;

        private void Start()
        {
            Collider[] targets = Physics.OverlapSphere(transform.position, targetScanRadius, LayerMask.GetMask("Player"));
            if (targets.Length > 0)
            {
                direction = Utility.GetDirection(transform.position, targets[0].transform.position);
            }
            else
            {
                Destroy(gameObject);
            }

        }

        private void Update()
        {
            timeAlive += Time.deltaTime;

            if (timeAlive > lifeTime)
            {
                Destroy(gameObject);
            }

            transform.Translate(direction * Time.deltaTime * speed, Space.World);

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Unit target = other.GetComponent<Unit>();
                target.TakeDamage(Random.Range(minDamage, maxDamage));
                target.Knockback(Utility.GetDirection(transform.position, target.transform.position), knockbackForce);

            }
        }

    }
}
