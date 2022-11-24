using UnityEngine;

namespace LaceEmUp.Units
{
    public class EnemyBat : EnemyManager
    {
        private void Update()
        {
            if (Animator)
            {
                Animator.SetFloat("movement_speed", Rigidbody.velocity.magnitude);
            }
        }
    }
}
