using BehaviorDesigner.Runtime.Tasks;
using LaceEmUp.Units;
using UnityEngine;

public class BTIsTargetWithinRange : Conditional
{
    [SerializeField] private SharedEnemyManager actor;
    [SerializeField] private float successRange;

    public override TaskStatus OnUpdate()
    {
        if (Utility.IsWithinRange(transform.position, actor.Value.AIDestSetter.target.position, successRange))
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }
}
