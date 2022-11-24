using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using LaceEmUp.Units;
using UnityEngine;

public class BTIsTargetInLookDirection : Conditional
{
    [SerializeField] private SharedEnemyManager actor;

    private Vector2 directionToTarget;

    public override TaskStatus OnUpdate()
    {
        if (!actor.Value.AIDestSetter.target)
        {
            return TaskStatus.Failure;
        }

        directionToTarget = Utility.GetDirection(transform.position, actor.Value.AIDestSetter.target.position);

        if (directionToTarget.x > 0 && transform.eulerAngles.y == 0)
        {
            return TaskStatus.Success;
        }
        else if (directionToTarget.x < 0 && transform.eulerAngles.y == 180)
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }

}
