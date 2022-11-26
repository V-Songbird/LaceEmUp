using JamOff.Scripts.Managers;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MarioFireBall : ObjPooled
{
    public float aliveTime = 3f;
        
    private int maxJumps = 4;
    private Rigidbody myRigibody;
    private AudioSource thisAudioSource;
    
    private void OnEnable()
    {
        // Get player
        var player = GamePlayManager.Instance.Player;

        // Ignore player collider
        Physics.IgnoreCollision(GetComponent<Collider>(), player.GetComponent<Collider>());

        // Add force to fireball
        if (myRigibody == null) myRigibody = GetComponent<Rigidbody>();
        var dir = player.transform.Find("GFX").localScale.x;
        myRigibody.AddForce(Vector3.right * 5 * dir, ForceMode.Impulse);
        
        // Play sound
        GetComponent<AudioSource>().Play();
        
        // Remove after X seconds
        Invoke("TerminateImmediatelyAndSilently", aliveTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        // If collides with enemy then trigger action and disable
        if (other.gameObject.CompareTag("Enemy"))
        {
            // TODO: Add function to hurt enemy
            TerminateImmediatelyAndSilently();
            return;
        }

        // If collides with terrain then bounce
        if (GamePlayManager.Instance.Player_MovementController.whatIsGround ==
            (GamePlayManager.Instance.Player_MovementController.whatIsGround | (1 << other.gameObject.layer)))
            CheckForDisable();
        else
            Debug.Log(other.gameObject.layer);
    }

    private void CheckForDisable()
    {
        maxJumps--;

        if (maxJumps == 0) TerminateImmediatelyAndSilently();
    }

    public override void TerminateImmediatelyAndSilently()
    {
        base.TerminateImmediatelyAndSilently();
        maxJumps = 4;
        myRigibody.velocity = Vector3.zero;
        transform.position = Vector3.zero;
    }
}