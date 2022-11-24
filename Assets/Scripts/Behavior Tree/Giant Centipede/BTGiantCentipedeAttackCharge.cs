using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Unity.Math;
using DG.Tweening;
using LaceEmUp.Units;
using System.Collections;
using UnityEngine;

namespace LaceEmUp.BehaviorDesigner
{
    public class BTGiantCentipedeAttackCharge : Action
    {

        [SerializeField] private SharedEnemyManager actor;
        [SerializeField] private Transform startPosition;
        [SerializeField] private Transform endPosition;
        [SerializeField] private float minAbilityCooldown;
        [SerializeField] private float maxAbilityCooldown;
        [SerializeField] private float minChargeDuration;
        [SerializeField] private float maxChargeDuration;
        [SerializeField] private float minTurnDuration;
        [SerializeField] private float maxTurnDuration;
        [SerializeField] private float chargeForce;
        [SerializeField] private float minDamage;
        [SerializeField] private float maxDamage;
        [SerializeField] private bool  isInStartPosition = true;
        [SerializeField] private Vector2 knockbackForce = new(10, 5);

        private bool isFinished;
        private bool isOnCooldown;
        private bool canUseAbility = true;
        private bool isAbilityActive;
        private LayerMask chargeLayerMask;

        // Cache

        private Collider[] targetsHit;
        private Unit target;

        // Cache

        public override void OnAwake()
        {
            chargeLayerMask = LayerMask.GetMask("Player");
        }

        public override TaskStatus OnUpdate()
        {

            if (isFinished)
            {
                isFinished    = false;
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
            canUseAbility   = false;
            isAbilityActive = true;

            float chargeDuration = 0;

            while (chargeDuration < Random.Range(minChargeDuration, maxChargeDuration))
            {
                chargeDuration += Time.deltaTime;


                // @TODO: CHECK POSITIONS - ARE THEY LOCAL?
                Debug.Log(startPosition.position);
                Debug.Log(endPosition.position);
                
                if (isInStartPosition)
                {
                    // @TODO: CHECK POSITIONS - ARE THEY LOCAL?
                    actor.Value.Rigidbody.position = Vector2.Lerp(startPosition.position, endPosition.position, chargeDuration);
                    
                }
                else
                {
                    actor.Value.Rigidbody.position = Vector2.Lerp(endPosition.position, startPosition.position, chargeDuration);
                }
                
                if (isAbilityActive)
                {
                    targetsHit = Physics.OverlapBox(actor.Value.TriggerCollider.bounds.center, actor.Value.TriggerCollider.bounds.extents, Quaternion.identity, chargeLayerMask);
                    if (targetsHit.Length > 0)
                    {
                        isAbilityActive = false;
                        target = targetsHit[0].GetComponent<Unit>();
                        target.TakeDamage(Random.Range(minDamage, maxDamage));
                        target.Knockback(Utility.GetDirection(transform.position, target.transform.position), knockbackForce);
                    }
                }

                yield return null;
            }

            Vector3 turnDirection;

            if (isInStartPosition)
            {
                isInStartPosition = false;
                turnDirection   = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
            }
            else
            {
                isInStartPosition = true;
                turnDirection   = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
            }

            actor.Value.GFX.transform.DORotate(turnDirection, Random.Range(minTurnDuration, maxTurnDuration));

            isFinished = true;

            StartCoroutine(DoCooldown());
        }

        private IEnumerator DoCooldown()
        {
            isOnCooldown = true;
            yield return new WaitForSeconds(Random.Range(minAbilityCooldown, maxAbilityCooldown));
            isOnCooldown = false;
        }

    }
}
