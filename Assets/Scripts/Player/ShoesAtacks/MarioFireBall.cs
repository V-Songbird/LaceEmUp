using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JamOff.Scripts.Managers;

public class MarioFireBall : ObjPooled
{
    Rigidbody myRigibody;

    int maxJumps = 4;

    private void OnEnable()
    {
        if (myRigibody == null)
        {
            myRigibody = GetComponent<Rigidbody>();
        }

        float dir = GamePlayManager.Instance.Player.transform.localScale.x;
        myRigibody.AddForce(Vector3.right * 10 * dir, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (GamePlayManager.Instance.Player_MovementController.whatisGround == (GamePlayManager.Instance.Player_MovementController.whatisGround | (1 << other.gameObject.layer)))
        {
            CheckForDisable();
        }
        else
        {
            Debug.Log(other.gameObject.layer);
        }
    }

    void CheckForDisable()
    {
        maxJumps--;

        if (maxJumps == 0)
        {
            TerminateImmediatelyAndSilently();
        }
    }

    public override void TerminateImmediatelyAndSilently()
    {
        base.TerminateImmediatelyAndSilently();
        maxJumps = 4;
        myRigibody.velocity = Vector3.zero;
        transform.position = Vector3.zero;
    }
}
