using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using LaceEmUp.Units;
using System.Collections;
using UnityEngine;

public class BTRockySmashAttack : Action
{

    [SerializeField] private SharedEnemyManager actor;

    private bool isFinished;
    private bool canSmash;
    private bool isSmashing;

    Vector2 direction;

    public override void OnStart()
    {
        canSmash = true;
        isFinished = false;
    }

    public override TaskStatus OnUpdate()
    {
        if (isFinished)
        {
            return TaskStatus.Success;
        }

        if (!actor.Value.AIDestSetter.target || Utility.IsOutOfRange(transform.position, actor.Value.transform.position, actor.Value.OutOfRangeDistance))
        {
            return TaskStatus.Failure;
        }

        if (canSmash)
        {
            StartCoroutine(DoSmash());
        }

        return TaskStatus.Running;
    }

    private IEnumerator DoSmash()
    {
        canSmash = false;
        isSmashing = true;

        Camera.main.DOComplete();
        Camera.main.DOShakePosition(0.3f, 1f).SetDelay(0.50f);
        transform.DORotate(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -90), 1).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            isSmashing = false;
            transform.DORotate(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0), 3).SetDelay(1).OnComplete(() =>
            {
                isFinished = true;
            });
        });

        while(!isFinished)
        {
            yield return null;
        }
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (isSmashing && collision.collider.CompareTag("Player"))
        {
            Unit target = collision.collider.GetComponent<Unit>();
            target.TakeDamage(Random.Range(actor.Value.MinDamage, actor.Value.MaxDamage));

            direction = Utility.GetDirection(transform.position, actor.Value.AIDestSetter.target.position);
            target.Rigidbody.AddForce(new Vector2(direction.x * actor.Value.KnockbackForce.x, actor.Value.KnockbackForce.y), ForceMode.Impulse);
        }
    }

}
