using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using LaceEmUp.Units;
using UnityEngine;

public class BTIsTargetPastYThreshold : Conditional
{
    [SerializeField] private SharedEnemyManager actor;
    [SerializeField] private float upYThreshold;
    [SerializeField] private float downYThreshold;

    private float actorYPosition;
    private float targetYPosition;

    public override TaskStatus OnUpdate()
    {
        if (!actor.Value.AIDestSetter.target)
        {
            return TaskStatus.Failure;
        }

        actorYPosition  = transform.position.y;
        targetYPosition = actor.Value.AIDestSetter.target.position.y;

        if (targetYPosition > actorYPosition + upYThreshold)
        {
            return TaskStatus.Success;
        }
        else if (targetYPosition < actorYPosition - downYThreshold)
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }

}
