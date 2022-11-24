using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using UnityEngine;

namespace LaceEmUp.BehaviorDesigner
{
    public class BTGiantCentipedeAttackThunderbolt : Action
    {

        [SerializeField] private Units.SharedEnemyManager actor;
        [SerializeField] private GameObject thunderboltCloudPrefab;
        [SerializeField] private float abilityCooldown;
        [SerializeField] private float abilityDuration;
        [SerializeField] private float channelDuration;
        [SerializeField] private float spawnYPosition;

        private bool isFinished;
        private bool isOnCooldown;
        private bool canUseAbility = true;

        #region Cache
        private Vector2 spawnPosition;
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

            yield return waitForChannelDuration;


            spawnPosition = new Vector2(
                actor.Value.AIDestSetter.target.transform.position.x,
                spawnYPosition);
            Object.Instantiate(thunderboltCloudPrefab, spawnPosition, Quaternion.identity);

            isFinished = true;

            isOnCooldown = true;
            yield return waitForAbilityCooldown;
            isOnCooldown = false;
        }

    }
}