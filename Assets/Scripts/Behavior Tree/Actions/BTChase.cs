using BehaviorDesigner.Runtime.Tasks;
using LaceEmUp.Units;
using UnityEngine;

namespace LaceEmUp.BehaviorDesigner
{
    public class BTChase : Action
    {
        [SerializeField] private SharedEnemyManager actor;
        [SerializeField] private float successRange;

        

        public override TaskStatus OnUpdate()
        {

            if (Utility.IsWithinRange(transform.position, actor.Value.AIDestSetter.target.position, successRange))
            {
                return TaskStatus.Success;
            }

            if (Utility.IsOutOfRange(transform.position, actor.Value.AIDestSetter.target.position, actor.Value.OutOfRangeDistance))
            {
                return TaskStatus.Failure;
            }

            return TaskStatus.Running;

        }
    }
}
