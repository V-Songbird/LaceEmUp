using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody;
using LaceEmUp.Units;
using System.Collections;
using UnityEngine;

namespace LaceEmUp.BehaviorDesigner
{
    public class BTChargeAttack : Action
    {
        [SerializeField] private SharedEnemyManager actor;
        [SerializeField] private string collisionTag;
        [SerializeField] private float force;

        private bool canCharge;
        private bool isFinished;
        private bool hitTarget;

        public override void OnStart()
        {
            actor.Value.CanMove = false;
            canCharge           = true;
            isFinished          = false;
            hitTarget = false;
        }

        public override TaskStatus OnUpdate()
        {
            if (isFinished)
            {
                return TaskStatus.Success;
            }

            if (canCharge)
            {
                StartCoroutine(DoCharge());
            }

            return TaskStatus.Running;
        }

        private IEnumerator DoCharge()
        {
            canCharge = false;

            actor.Value.Rigidbody.AddForce(Utility.GetDirection(transform.position, actor.Value.AIDestSetter.target.position) * force,ForceMode.Impulse);

            yield return new WaitForSeconds(3);
            isFinished = true;
            actor.Value.CanMove = true;
            actor.Value.TriggerCollider.enabled = true;
        }

        public override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(collisionTag) && !hitTarget)
            {
                hitTarget = true;
                actor.Value.TriggerCollider.enabled = false;
                Unit target = other.GetComponent<Unit>();
                target.TakeDamage(Random.Range(actor.Value.MinDamage, actor.Value.MaxDamage));
            }
        }

    }
}
