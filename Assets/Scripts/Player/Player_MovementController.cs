using UnityEngine;
using JamOff.Scripts.Managers;
using DG.Tweening;

public class Player_MovementController : MonoBehaviour
{
    [SerializeField] private LaceEmUp.Units.Unit player;
    [SerializeField] private int jumps;
    [SerializeField] float jumpForce;

    private float horizontalInput;
    private Vector3 movementInput;
    private bool facingRight = true;

    public bool isGrounded;
    public LayerMask whatIsGround;
    
    private int resetJumps;

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
            horizontalInput = Input.GetAxis("Horizontal");
            movementInput = new Vector3(horizontalInput, 0, 0);

            player.Rigidbody.MovePosition(transform.position + movementInput * Time.deltaTime * player.MovementSpeed);

            if (horizontalInput != 0)
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
        jumps      = resetJumps;
        //player.Rigidbody.mass = 1;
    }

    bool needToFlip()
    {
        if ((facingRight == false && horizontalInput > 0) || facingRight == true && horizontalInput < 0)
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
