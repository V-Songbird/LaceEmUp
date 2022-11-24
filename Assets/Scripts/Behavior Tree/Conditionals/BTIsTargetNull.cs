using BehaviorDesigner.Runtime.Tasks;
using LaceEmUp.Units;
using UnityEngine;

public class BTIsTargetNull : Conditional
{
    [SerializeField] private SharedEnemyManager actor;

    public override TaskStatus OnUpdate()
    {
        if (!actor.Value.AIDestSetter.target)
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }
}
