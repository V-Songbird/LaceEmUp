using System.Collections;
using UnityEngine;

namespace LaceEmUp
{
    public class LavaSpawn : MonoBehaviour
    {

        [SerializeField] private float duration;
        [SerializeField] private float tickCooldown;
        [SerializeField] private float minDamage;
        [SerializeField] private float maxDamage;

        private bool canTick = true;

        private void OnEnable()
        {
            Invoke("TurnOff", duration);
        }

        private void OnTriggerStay(Collider other)
        {
            if (canTick && other.CompareTag("Player"))
            {
                Units.Unit target = other.GetComponent<Units.Unit>();
                target.TakeDamage(Random.Range(minDamage, maxDamage));
                StartCoroutine(DoCooldown());
            } 
        }

        private IEnumerator DoCooldown()
        {
            canTick = false;
            yield return new WaitForSeconds(tickCooldown);
            canTick = true;
        }

        private void TurnOff()
        {
            gameObject.SetActive(false);
        }

    }
}