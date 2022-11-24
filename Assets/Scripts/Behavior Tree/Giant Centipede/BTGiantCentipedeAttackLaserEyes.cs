using BehaviorDesigner.Runtime.Tasks;
using LaceEmUp.Units;
using System.Collections;
using UnityEngine;

namespace LaceEmUp.BehaviorDesigner
{
    public class BTGiantCentipedeAttackLaserEyes : Action
    {
        private enum ShootDirection
        {
            Up,
            Down,
            Target
        }

        [SerializeField] private SharedEnemyManager actor;
        [SerializeField] private Transform laserEyesPivot;
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private float abilityCooldown;
        [SerializeField] private float abilityDuration;
        [SerializeField] private float channelDuration;
        [SerializeField] private float minDamage;
        [SerializeField] private float maxDamage;
        [SerializeField] private Vector2 knockbackForce;

        private bool isFinished;
        private bool isOnCooldown;
        private bool isAbilityActive;
        private bool canUseAbility = true;

        #region Cache
        private Vector2 direction;
        private RaycastHit hit;
        private Unit target;
        private float laserEyesRotation;
        private WaitForSeconds waitForChannelDuration;
        private WaitForSeconds waitForAbilityCooldown;
        #endregion

        public override void OnAwake()
        {
            waitForChannelDuration = new WaitForSeconds(channelDuration);
            waitForAbilityCooldown = new WaitForSeconds(abilityCooldown);
        }

        public override TaskStatus OnUpdate()
        {

            if (isFinished)
            {
                isFinished = false;
                canUseAbility = true;
                return TaskStatus.Success;
            }

            if (isOnCooldown)
            {
                return TaskStatus.Failure;
            }

            if (canUseAbility)
            {
                StartCoroutine(DoAbility());
            }

            return TaskStatus.Running;
        }

        private IEnumerator DoAbility()
        {
            canUseAbility = false;
            isAbilityActive = true;

            float duration = abilityDuration;

            yield return waitForChannelDuration;
            laserEyesRotation = 0;

            lineRenderer.SetPosition(0, laserEyesPivot.position);
            lineRenderer.gameObject.SetActive(true);

            while (duration > 0)
            {
                duration -= Time.deltaTime;

                ShootLaser(ShootDirection.Down);

                yield return null;
            }
            isAbilityActive = false;

            lineRenderer.gameObject.SetActive(false);

            yield return waitForChannelDuration;

            lineRenderer.gameObject.SetActive(true);

            isAbilityActive = true;
            duration = abilityDuration;

            laserEyesRotation = 140;
            while (duration > 0)
            {
                duration -= Time.deltaTime;

                ShootLaser(ShootDirection.Up);

                yield return null;
            }
            isAbilityActive = false;

            lineRenderer.gameObject.SetActive(false);

            isFinished = true;

            isOnCooldown = true;
            yield return waitForAbilityCooldown;
            isOnCooldown = false;
        }

        private void ShootLaser(ShootDirection shootDirection)
        {

            switch (shootDirection)
            {
                case ShootDirection.Up:
                    laserEyesRotation -= 0.75f;
                    laserEyesPivot.eulerAngles = new Vector3(laserEyesPivot.eulerAngles.x, laserEyesPivot.eulerAngles.y, laserEyesRotation);
                    break;
                case ShootDirection.Down:
                    laserEyesRotation += 0.75f;
                    laserEyesPivot.eulerAngles = new Vector3(laserEyesPivot.eulerAngles.x, laserEyesPivot.eulerAngles.y, laserEyesRotation);
                    break;
                case ShootDirection.Target:
                    direction = Utility.GetDirection(laserEyesPivot.position, actor.Value.AIDestSetter.target.position);
                    laserEyesPivot.up = direction;
                    break;
            }


            if (Physics.Raycast(laserEyesPivot.position, laserEyesPivot.up, out hit, 100))
            {
                //Debug.DrawRay(laserEyesPivot.position, laserEyesPivot.up * hit.distance, Color.red, 0.1f);

                lineRenderer.SetPosition(1, hit.point);

                if (hit.collider.CompareTag("Player"))
                {
                    target = hit.collider.GetComponent<Unit>();
                    target.TakeDamage(Random.Range(minDamage, maxDamage));
                    target.Knockback(laserEyesPivot.up, knockbackForce);
                }

            }
        }

    }
}
