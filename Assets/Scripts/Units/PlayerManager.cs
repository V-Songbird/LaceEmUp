using System.Collections;
using UnityEngine;

namespace LaceEmUp.Units
{
    public class PlayerManager : Unit
    {
        [SerializeField] private float   attackDistance;
        [SerializeField] private float   attackCooldown;
        [SerializeField] private float   minDamage;
        [SerializeField] private float   maxDamage;
        [SerializeField] private Vector2 knockbackForce = new Vector2(5, 0.5f);

        private bool canAttack = true;

        private LayerMask attackLayerMask;

        private void Start()
        {
            attackLayerMask = LayerMask.GetMask("Enemy");
        }

        public override void Attack()
        {
            if (canAttack)
            {
                StartCoroutine(doAttack());
            }
        }

        private IEnumerator doAttack()
        {
            canAttack = false;

            Vector2 attackDirection = new Vector2(GFX.localScale.x * attackDistance, 0);
            Debug.DrawRay(transform.position, attackDirection, Color.magenta, 2f);
            RaycastHit[] hits = Physics.RaycastAll(transform.position, attackDirection, attackDistance, attackLayerMask);
            if (hits.Length > 0)
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].collider.TryGetComponent(out EnemyManager value))
                    {
                        value.TakeDamage(Random.Range(minDamage, maxDamage));
                        value.Knockback(attackDirection, knockbackForce);
                    }
                }
            }

            yield return new WaitForSeconds(attackCooldown);

            canAttack = true;
        }

        protected override void Die()
        {
            base.Die();
            Time.timeScale = 0.1f;
            Time.fixedDeltaTime *= Time.timeScale;
            MovementSpeed = 0;
        }

    }
}
