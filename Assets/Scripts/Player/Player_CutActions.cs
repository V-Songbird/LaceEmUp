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
        GamePlayManager.Instance.Player_MovementController.playerRb.velocity = Vector3.zero;
    }

    public void GetBackMovement()
    {
        canMove = true;
    }
}
