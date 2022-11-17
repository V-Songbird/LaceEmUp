using BehaviorDesigner.Runtime;
using Pathfinding;
using UnityEngine;

namespace LaceEmUp.Units
{
    [System.Serializable]
    public class EnemyManager : Unit
    {

        #region Initilization
        [Header("Enemy Manager")]
        [Header("Dependencies")]
        [SerializeField] private AIDestinationSetter aiDestSetter;
        [SerializeField] private AIPath              aiPath;
        [SerializeField] private Collider            triggerCollider;

        [Header("Stats")]
        [SerializeField] private float attackRange;
        [SerializeField] private float attackCooldown;
        [SerializeField] private float minDamage;
        [SerializeField] private float maxDamage;
        [SerializeField] private float knockbackForce;
        [SerializeField] private float scanRadius;
        [SerializeField] private float outOfRangeDistance;
        [SerializeField] private int   spiritOrbs;
        #endregion

        #region Encapsulation
        public AIDestinationSetter AIDestSetter    { get => aiDestSetter;    }
        public AIPath              AIPath          { get => aiPath;          }
        public Collider            TriggerCollider { get => triggerCollider; }

        public float AttackRange        { get => attackRange;        }
        public float AttackCooldown     { get => attackCooldown;     }
        public float MinDamage          { get => minDamage;          }
        public float MaxDamage          { get => maxDamage;          }
        public float KnockbackForce     { get => knockbackForce;     }
        public float ScanRadius         { get => scanRadius;         }
        public float OutOfRangeDistance { get => outOfRangeDistance; }
        public float SpiritOrbs         { get => spiritOrbs;         }
        #endregion

        #region Subscriptions
        private void OnEnable()
        {
            OnMovementSpeedChanged += OnMovementSpeedChangedCallback;
            OnCanMoveChanged       += OnCanMoveChangedCallback;
        }

        private void OnDisable()
        {
            OnMovementSpeedChanged -= OnMovementSpeedChangedCallback;
            OnCanMoveChanged       -= OnCanMoveChangedCallback;
        }

        private void OnCanMoveChangedCallback(bool value)
        {
            aiPath.canMove = value;
        }

        private void OnMovementSpeedChangedCallback(float value)
        {
            aiPath.maxSpeed = value;
        }
        #endregion

        private void Update()
        {
            Animator.SetFloat("movement_speed", Rigidbody.velocity.magnitude);
        }

        protected override void Attack()
        {
            base.Attack();
        }

        protected override void Die()
        {
            Destroy(gameObject);
        }

    }

   [System.Serializable]
    public class SharedEnemyManager : SharedVariable<EnemyManager>
    {
        public static implicit operator SharedEnemyManager(EnemyManager value) { return new SharedEnemyManager { Value = value }; }
    }
}

