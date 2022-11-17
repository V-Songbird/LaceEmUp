using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    Player_MovementController player_MovementController;

    private void Start()
    {
        player_MovementController = GetComponentInParent<Player_MovementController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (player_MovementController.whatIsGround == (player_MovementController.whatIsGround | (1 << other.gameObject.layer)))
        {
            player_MovementController.ResetJumps();
        }
    }
}
