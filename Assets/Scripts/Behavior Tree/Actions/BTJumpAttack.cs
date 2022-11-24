using BehaviorDesigner.Runtime.Tasks;
using LaceEmUp.Units;
using System.Collections;
using UnityEngine;

public class BTJumpAttack : Action
{

    [SerializeField] private SharedEnemyManager actor;
    [SerializeField] private string  attackableTag  = "Player";
    [SerializeField] private float   jumpCooldown   = 4;
    [SerializeField] private Vector2 jumpForce     = new Vector2(8, 6);
    [SerializeField] private Vector2 knockbackForce = new Vector2(5, 2.5f);

    private bool isFinished;
    private bool canJump;

    Vector2 direction;

    public override void OnStart()
    {
        if (actor.Value.TriggerCollider.enabled == false)
        {
            actor.Value.TriggerCollider.enabled = true;
        }
        StartCoroutine(SetCanJumpToTrue());
    }

    public override TaskStatus OnUpdate()
    {
        if (isFinished)
        {
            return TaskStatus.Success;
        }

        if (Utility.IsOutOfRange(transform.position, actor.Value.transform.position, actor.Value.OutOfRangeDistance))
        {
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

        direction = Utility.GetDirection(transform.position, actor.Value.AIPath.steeringTarget);
        actor.Value.Rigidbody.AddForce(new Vector2(direction.x * jumpForce.x, jumpForce.y), ForceMode.Impulse);

        yield return new WaitForSeconds(GetRandomJumpCooldown(offset: 1));
        actor.Value.TriggerCollider.enabled = true;

        canJump = true;
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(attackableTag))
        {
            actor.Value.TriggerCollider.enabled = false;
            Unit target = other.GetComponent<Unit>();
            target.TakeDamage(Random.Range(actor.Value.MinDamage, actor.Value.MaxDamage));
            target.Rigidbody.AddForce(new Vector2(direction.x * knockbackForce.x, direction.y + knockbackForce.y), ForceMode.Impulse);
        }
    }

    private IEnumerator SetCanJumpToTrue()
    {
        yield return new WaitForSeconds(GetRandomJumpCooldown(offset: 1));
        canJump = true;
    }

    private float GetRandomJumpCooldown(float offset)
    {
        return Random.Range(jumpCooldown - offset, jumpCooldown + offset);
    }


}
