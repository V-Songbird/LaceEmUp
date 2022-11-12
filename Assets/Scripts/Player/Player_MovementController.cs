using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JamOff.Scripts.Managers;

public class Player_MovementController : MonoBehaviour
{
    [SerializeField] float speedMovement;
    [SerializeField] float jumpForce;
    float moveInput;
    bool facingRight = true;



    public bool isGrounded;
    public LayerMask whatisGround;
    [SerializeField] int jumps;
    int resetJumps;

    [HideInInspector] public Rigidbody playerRb;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        resetJumps = jumps;
    }

    private void Update()
    {
        if (GamePlayManager.Instance.Player_CutActions.canMove)
        {

            if (Input.GetButtonDown("Jump"))
            {
                MakeJump();
            }

            if (playerRb.velocity.y < 0)
            {
                playerRb.velocity += Vector3.up * Physics.gravity.y * 3f * Time.deltaTime;
            }
            else if (playerRb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                playerRb.velocity += Vector3.up * Physics.gravity.y * 2f * Time.deltaTime;
            }
        }
    }

    private void FixedUpdate()
    {
        if (GamePlayManager.Instance.Player_CutActions.canMove)
        {
            moveInput = Input.GetAxis("Horizontal");
            playerRb.velocity = new Vector2(moveInput * speedMovement, playerRb.velocity.y);

            if (moveInput != 0)
            {
                GamePlayManager.Instance.Player_OtherActions.CancelAllActions();
            }

            if (needToFlip())
            {
                Flip();
            }
        }

    }


    void MakeJump()
    {
        if (jumps > 0)
        {
            GamePlayManager.Instance.Player_OtherActions.CancelAllActions();
            isGrounded = false;
            playerRb.velocity = Vector3.up * jumpForce;
            jumps--;
        }
    }

    public void ResetJumps()
    {
        isGrounded = true;
        jumps = resetJumps;
        playerRb.mass = 1;
    }







    bool needToFlip()
    {
        if ((facingRight == false && moveInput > 0) || facingRight == true && moveInput < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
