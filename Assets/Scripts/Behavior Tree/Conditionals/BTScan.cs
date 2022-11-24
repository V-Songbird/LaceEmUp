using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using LaceEmUp.Units;
using BehaviorDesigner.Runtime;

namespace LaceEmUp.BehaviorDesigner
{
    public class BTScan : Conditional
    {
        [SerializeField] private SharedEnemyManager actor;
        [SerializeField] private LayerMask layerMask;

        [SerializeField] private bool useBTTarget;
        [SerializeField] private SharedTransform target;


        private Collider[] results = new Collider[0];

        public override void OnStart()
        {
            if (!useBTTarget)
            {
                target.Value = actor.Value.AIDestSetter.target;
            }
        }

        public override TaskStatus OnUpdate()
        {

            if (target.Value)
            {
                return TaskStatus.Success;
            }

            results = Physics.OverlapSphere(transform.position, actor.Value.ScanRadius, layerMask);
            if (results.Length > 0)
            {
                if (useBTTarget)
                {
                    target.Value = results[0].transform;
                }
                else
                {
                    actor.Value.AIDestSetter.target = results[0].transform;
                }
                
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;

        }
    }
}
