using UnityEngine;

public class AreaDamage : MonoBehaviour
{
    public float damageRadius = 5f; // Radius of the area damage
    public int damageAmount = 10; // Damage amount

    void Start()
    {
        // Apply damage to all enemies within the radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, damageRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                EnemyBehavior enemy = collider.GetComponent<EnemyBehavior>();
                if (enemy != null)
                {
                    enemy.KillEnemy();
                }
            }
        }

        // Destroy the area damage effect after a short delay
        Destroy(gameObject, 2f); // Adjust the delay as needed
    }
}