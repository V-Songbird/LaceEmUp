using UnityEngine;


namespace LaceEmUp.Units
{

    public class ThunderboltCloud : MonoBehaviour
    {

        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private float attackAfterSeconds;
        [SerializeField] private float minDamage;
        [SerializeField] private float maxDamage;
        [SerializeField] private Vector2 knockbackForce;

        private float timer;
        private float lineWidth;

        private void Start()
        {
            lineRenderer.SetPosition(0, transform.position);
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 100, LayerMask.GetMask("Ground")))
            {
                lineRenderer.SetPosition(1, hit.point);
            }
        }

        private void Update()
        {
            timer += Time.deltaTime;

            lineWidth += timer * 0.01f;
            lineRenderer.widthMultiplier = lineWidth;

            if (timer > attackAfterSeconds * 0.8f)
            {
                lineRenderer.widthMultiplier = lineWidth * 2;
            }

            if (timer > attackAfterSeconds)
            {

                if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 100, LayerMask.GetMask("Player")))
                {
                    if (hit.collider.TryGetComponent(out Unit target))
                    {
                        target.TakeDamage(Random.Range(minDamage, maxDamage));
                        target.Knockback(new Vector2(Random.Range(-1f, 1f), 0.5f), knockbackForce);
                    }
                }

                Destroy(gameObject);
            }
        }
    }
    
}
