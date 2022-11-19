using DG.Tweening;
using UnityEngine;

namespace LaceEmUp.Units
{
    public class Unit : MonoBehaviour
    {
        #region Initilization
        [Header("Unit")]
        [Header("Dependencies")]
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform _gfx;
        [SerializeField] private Animator _animator;

        [Header("Stats")]
        [SerializeField] private float health;
        [SerializeField] private float maxHealth;
        [SerializeField] private float movementSpeed;
        #endregion

        #region Encapsulation
        private bool isDead;
        private bool canMove = true;

        public    Transform GFX       { get => _gfx; }
        public    Rigidbody Rigidbody { get => _rigidbody; }
        protected Animator  Animator  { get => _animator; }

        public float Health        { get => health;        protected set { health        = value; OnHealthChanged?.Invoke(value);        } }
        public float MaxHealth     { get => maxHealth;     protected set { maxHealth     = value; OnMaxHealthChanged?.Invoke(value);     } }
        public float MovementSpeed { get => movementSpeed; protected set { movementSpeed = value; OnMovementSpeedChanged?.Invoke(value); } }

        public bool IsDead  { get => isDead; }
        public bool CanMove { get => canMove; set { canMove = value; OnCanMoveChanged?.Invoke(value); } }

        protected System.Action<float> OnHealthChanged;
        protected System.Action<float> OnMaxHealthChanged;
        protected System.Action<float> OnMovementSpeedChanged;
        protected System.Action<bool>  OnCanMoveChanged;
        #endregion

        private void OnDisable()
        {
            transform.DOComplete();
        }

        public void TakeDamage(float value)
        {
            if (IsDead)
            {
                return;
            }

            _gfx.transform.DOComplete();
            _gfx.transform.DOPunchScale(-Vector3.one * 0.5f, 0.15f);

            health -= value;

            if (health <= 0)
            {
                health = 0;
                Die();
            }
        }

        public void Heal(float value)
        {
            if (IsDead || health >= maxHealth)
            {
                return;
            }

            if (health + value >= maxHealth)
            {
                health = maxHealth;
                return;
            }

            health += value;
        }

        public void Knockback(Vector2 direction, Vector2 force)
        {
            direction = new Vector2(direction.x * force.x,  direction.y + force.y);
            _rigidbody.AddForce(direction, ForceMode.Impulse);
        }

        public void Resurrect(float healAmount)
        {
            isDead = false;
            Heal(healAmount);
        }

        public virtual void Attack()
        {

        }

        protected virtual void Die()
        {
            isDead = true;
        }

    }
}
