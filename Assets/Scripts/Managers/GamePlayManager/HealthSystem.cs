using UnityEngine;


public class HealthSystem : MonoBehaviour
{

    public IndividualHeart ActualHealt;

    public float healtPerHeart = 20f;
    public float damageTest;

    public void ReciveDamage(float damage)
    {
        ActualHealt.RecibeDamage(damage);
    }

    public void Heal(float amount)
    {
        ActualHealt.Heal(amount);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            ReciveDamage(damageTest);
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Heal(damageTest);
        }
    }
}
