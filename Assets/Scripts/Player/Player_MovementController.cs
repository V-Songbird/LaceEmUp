using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    Rigidbody playerRb;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        resetJumps = jumps;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            MakeJump();
        }
    }

    private void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");
        playerRb.velocity = new Vector2(moveInput * speedMovement, playerRb.velocity.y);

        if (needToFlip())
        {
            Flip();
        }
    }


    void MakeJump()
    {
        if (jumps > 0)
        {
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
