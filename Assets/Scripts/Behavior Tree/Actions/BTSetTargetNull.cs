using BehaviorDesigner.Runtime.Tasks;
using LaceEmUp.Units;
using UnityEngine;

public class BTSetTargetNull : Action
{
    [SerializeField] private SharedEnemyManager actor;

    public override void OnStart()
    {
        actor.Value.AIDestSetter.target = null;
    }
}
