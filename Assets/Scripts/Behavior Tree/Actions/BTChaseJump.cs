using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using LaceEmUp.Units;
using System.Collections;
using UnityEngine;

namespace LaceEmUp.BehaviorDesigner
{
    public class BTChaseJump : Action
    {

        [SerializeField] private SharedEnemyManager actor;
        [SerializeField] private float   jumpCooldown = 4;
        [SerializeField] private float   jumpDuration = 1;
        [SerializeField] private Vector2 jumpForce = new Vector2(4, 2.5f);

        Vector2 directionToTarget;

        private bool canJump = true;
        private bool isJumping;

        public override TaskStatus OnUpdate()
        {

            if (!actor.Value.AIDestSetter.target)
            {
                return TaskStatus.Failure;
            }

            if (!isJumping && Utility.IsWithinRange(transform.position, actor.Value.AIDestSetter.target.position, actor.Value.AttackRange))
            {
                return TaskStatus.Success;
            }

            if (
               !isJumping  
               && Utility.IsOutOfRange(transform.position, actor.Value.AIDestSetter.target.position, actor.Value.OutOfRangeDistance) 
               || Utility.IsOutOfRange(transform.position, actor.Value.SpawnLocation, actor.Value.OutOfRangeDistance)
               )
            {
                actor.Value.AIDestSetter.target = null;
                return TaskStatus.Failure;
            }

            if (canJump)
            {
                StartCoroutine(DoJump());
            }

            return TaskStatus.Running;
        }

        private IEnumerator DoJump()
        {
            canJump = false;
            isJumping = true;

            directionToTarget = Utility.GetDirection(transform.position, actor.Value.AIDestSetter.target.transform.position);
            actor.Value.Rigidbody.AddForce(directionToTarget * jumpForce, ForceMode.Impulse);
            yield return new WaitForSeconds(jumpDuration);
            Camera.main.DOComplete();
            Camera.main.DOShakePosition(0.15f, 0.75f);
            isJumping = false;

            yield return new WaitForSeconds(jumpCooldown);
            canJump = true;
        }

    }
}
