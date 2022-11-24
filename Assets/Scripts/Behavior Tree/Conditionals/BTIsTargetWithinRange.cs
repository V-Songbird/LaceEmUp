using BehaviorDesigner.Runtime.Tasks;
using LaceEmUp.Units;
using UnityEngine;

public class BTIsTargetWithinRange : Conditional
{
    [SerializeField] private SharedEnemyManager actor;
    [SerializeField] private bool  useActorRange;
    [SerializeField] private float successRange;

    public override TaskStatus OnUpdate()
    {
        if (!actor.Value.AIDestSetter.target)
        {
            return TaskStatus.Failure;
        }

        if (useActorRange)
        {
            if (Utility.IsWithinRange(transform.position, actor.Value.AIDestSetter.target.position, actor.Value.AttackRange))
            {
                return TaskStatus.Success;
            }
        }
        else
        {
            if (Utility.IsWithinRange(transform.position, actor.Value.AIDestSetter.target.position, successRange))
            {
                return TaskStatus.Success;
            }
        }


        return TaskStatus.Failure;
    }
}
