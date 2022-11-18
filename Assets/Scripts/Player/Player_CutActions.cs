using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JamOff.Scripts.Managers;

public class Player_CutActions : MonoBehaviour
{
    [HideInInspector] public bool canMove = true;

    public void BlockMovement()
    {
        canMove = false;
        GamePlayManager.Instance.PlayerManager.Rigidbody.velocity = Vector3.zero;
    }

    public void GetBackMovement()
    {
        canMove = true;
    }

    public void DisablePhysicsAndMovements()
    {
        BlockMovement();
        GamePlayManager.Instance.Player.GetComponent<CapsuleCollider>().enabled = false;
        GamePlayManager.Instance.PlayerManager.Rigidbody.useGravity = false;
    }
    public void GetBackPhysicsAndMovement()
    {
        GetBackMovement();
        GamePlayManager.Instance.Player.GetComponent<CapsuleCollider>().enabled = true;
        GamePlayManager.Instance.PlayerManager.Rigidbody.useGravity = true;
    }
}
