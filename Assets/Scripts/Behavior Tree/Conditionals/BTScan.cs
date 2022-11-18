using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using LaceEmUp.Units;

namespace LaceEmUp.BehaviorDesigner
{
    public class BTScan : Conditional
    {
        [SerializeField] private SharedEnemyManager actor;
        [SerializeField] private LayerMask layerMask;

        private Collider[] results = new Collider[0];

        public override TaskStatus OnUpdate()
        {
            results = Physics.OverlapSphere(transform.position, actor.Value.ScanRadius, layerMask);
            if (results.Length > 0)
            {
                actor.Value.AIDestSetter.target = results[0].transform;
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }
    }
}
