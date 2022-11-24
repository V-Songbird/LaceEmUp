using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using LaceEmUp.Units;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace LaceEmUp.BehaviorDesigner
{
    public class BTGiantCentipedeAttackFloorIsLava : Action
    {

        [SerializeField] private SharedEnemyManager actor;
        [SerializeField] private float abilityCooldown;
        [SerializeField] private float spawnSpeed;
        [SerializeField] private int   minSpawns;
        [SerializeField] private int   maxSpawns;
    
        private Transform lavaSpawnsRoot;
        private List<LavaSpawn> lavaSpawns = new();
        private LavaSpawn randomLavaSpawn;

        private float spawnAmount;

        private bool isFinished;
        private bool canUseAbility = true;
        private bool isOnCooldown;

        public override void OnAwake()
        {
            lavaSpawnsRoot = GameObject.Find("Lava Spawns Root").transform;
            lavaSpawnsRoot.GetComponentsInChildren(true, lavaSpawns);
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

            spawnAmount = Random.Range(minSpawns, maxSpawns);
            int spawnCounter = 0;
            while (spawnAmount >= spawnCounter)
            {
                randomLavaSpawn = GetRandomLavaSpawn();
                while (randomLavaSpawn.gameObject.activeSelf)
                {
                    randomLavaSpawn = GetRandomLavaSpawn();
                }

                randomLavaSpawn.gameObject.SetActive(true);
                spawnCounter++;
                yield return new WaitForSeconds(spawnSpeed);
            }

            isFinished = true;
            StartCoroutine(DoCooldown());
        }

        private IEnumerator DoCooldown()
        {
            isOnCooldown = true;
            yield return new WaitForSeconds(abilityCooldown);
            isOnCooldown = false;

        }

        private LavaSpawn GetRandomLavaSpawn()
        {
            return lavaSpawns[Random.Range(0, lavaSpawns.Count)];
        }
    }
}
