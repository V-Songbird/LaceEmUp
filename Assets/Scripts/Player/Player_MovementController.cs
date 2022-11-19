using UnityEngine;
using JamOff.Scripts.Managers;
using DG.Tweening;

public class Player_MovementController : MonoBehaviour
{
    [SerializeField] private LaceEmUp.Units.Unit player;
    [SerializeField] float jumpForce;
    float moveInput;
    bool facingRight = true;

    public bool isGrounded;
    public LayerMask whatIsGround;
    [SerializeField] int jumps;
    int resetJumps;

    private void Start()
    {
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


        }
    }

    private void FixedUpdate()
    {
        if (GamePlayManager.Instance.Player_CutActions.canMove)
        {
            moveInput = Input.GetAxis("Horizontal");
            player.Rigidbody.velocity = new Vector2(moveInput * player.MovementSpeed, player.Rigidbody.velocity.y);

            if (moveInput != 0)
            {
                GamePlayManager.Instance.Player_OtherActions.CancelAllActions();
            }

            if (player.Rigidbody.velocity.y < 0)
            {
                player.Rigidbody.velocity += Vector3.up * Physics.gravity.y * 3f * Time.deltaTime;
            }
            else if (player.Rigidbody.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                player.Rigidbody.velocity += Vector3.up * Physics.gravity.y * 2f * Time.deltaTime;
            }

            if (needToFlip() && !player.IsDead)
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
            player.Rigidbody.velocity = Vector3.up * jumpForce;
            jumps--;

            player.GFX.DOComplete();
            player.GFX.DOPunchScale(Vector3.up * 0.25f, 0.15f);
        }
    }

    public void ResetJumps()
    {
        player.GFX.DOComplete();
        player.GFX.DOPunchScale(new Vector3(0.3f, -0.3f), 0.20f);
        isGrounded = true;
        jumps = resetJumps;
        player.Rigidbody.mass = 1;
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
        float flipScaleX = facingRight ? 1 : -1;
        player.GFX.DOComplete();
        player.GFX.DOScaleX(flipScaleX, 0.25f);
    }
}
