using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using LaceEmUp.Units;
using System.Collections;
using UnityEngine;

public class BTTurnAround : Action
{

    [SerializeField] private SharedEnemyManager actor;
    [SerializeField] private float turnDuration;

    private bool isFinished;
    private bool isTurning;

    public override void OnStart()
    {
        isFinished = false;
        isTurning  = false;
    }

    public override TaskStatus OnUpdate()
    {
        if (isFinished)
        {
            return TaskStatus.Success;
        }

        if (!isTurning)
        {
            StartCoroutine(DoTurn());
        }

        return TaskStatus.Running;
    }

    private IEnumerator DoTurn()
    {
        isTurning = true;
        transform.DORotate(GetTurnDirection(), turnDuration);
        yield return new WaitForSeconds(turnDuration);
        isFinished = true;
    }

    private Vector3 GetTurnDirection()
    {
        if (transform.eulerAngles.y == 0)
        {
            return new Vector3(transform.eulerAngles.x, y: 180, transform.transform.eulerAngles.z);
        }
        else
        {
            return new Vector3(transform.eulerAngles.x, y: 0,   transform.transform.eulerAngles.z);
        }

    }

}
