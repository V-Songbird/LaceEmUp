using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using UnityEngine;


namespace LaceEmUp.BehaviorDesigner
{
    public class BTGiantCentipedeAttackSummonGhosts : Action
    {

        [SerializeField] private Units.SharedEnemyManager actor;
        [SerializeField] private GameObject ghostPrefab;
        [SerializeField] private float awakeCooldown;
        [SerializeField] private float abilityCooldown;
        [SerializeField] private float channelDuration;
        [SerializeField] private int   spawnAmount;

        private bool isFinished;
        private bool isOnCooldown;
        private bool canUseAbility = true;

        #region Cache
        private WaitForSeconds waitForChannelDuration;
        private WaitForSeconds waitForAbilityCooldown;
        #endregion

        public override void OnAwake()
        {
            waitForChannelDuration = new WaitForSeconds(channelDuration / spawnAmount);
            waitForAbilityCooldown = new WaitForSeconds(abilityCooldown);

            if (awakeCooldown > 0)
            {
                StartCoroutine(DoAwakeCooldown());
            }
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

            for (int i = 0; i < spawnAmount; i++)
            {
                Object.Instantiate(ghostPrefab, new Vector3(transform.position.x, transform.position.y + 2, -0.01f), Quaternion.identity);
                yield return waitForChannelDuration;
            }       

            isFinished = true;

            isOnCooldown = true;
            yield return waitForAbilityCooldown;
            isOnCooldown = false;
        }

        private IEnumerator DoAwakeCooldown()
        {
            isOnCooldown = true;
            yield return new WaitForSeconds(awakeCooldown);
            isOnCooldown = false;
        }

    }
}
