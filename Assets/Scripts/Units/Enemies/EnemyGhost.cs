using UnityEngine;

namespace LaceEmUp.Units
{
    public class EnemyGhost : EnemyManager
    {

        Unit target;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                target = other.GetComponent<Unit>();
                target.TakeDamage(Random.Range(MinDamage, MaxDamage));
                target.Knockback(Utility.GetDirection(transform.position, target.transform.position), KnockbackForce);
            }
        }
    }
}
