using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using LaceEmUp.Units;
using UnityEngine;


namespace LaceEmUp.BehaviorDesigner
{
    public class BTMoveTowardsTarget : Action
    {

        [SerializeField] private SharedEnemyManager actor;

        [SerializeField] private bool useBTTarget;
        [SerializeField] private SharedTransform target;

        Vector2 direction;

        public override void OnStart()
        {
            if (!useBTTarget)
            {
                target.Value = actor.Value.AIDestSetter.target;
            }
        }

        public override TaskStatus OnUpdate()
        {
            direction = Utility.GetDirection(transform.position, target.Value.position);

            actor.Value.Rigidbody.AddForce(direction * actor.Value.MovementSpeed);

            if (useBTTarget && actor.Value.Animator)
            {
                actor.Value.Animator.SetFloat("movement_speed", actor.Value.Rigidbody.velocity.magnitude);
            }

            return TaskStatus.Running;
        }

        public override void OnEnd()
        {
            if (useBTTarget && actor.Value.Animator)
            {
                actor.Value.Animator.SetFloat("movement_speed", 0);
            }
        }

    }
}
