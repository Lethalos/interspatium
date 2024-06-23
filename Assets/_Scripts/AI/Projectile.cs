using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 32f;
    public float upwardForce = 8f;
    public float lifetime = 5f;
    public int damage = 10;

    private void Start()
    {
        // Destroy the projectile after a certain time to avoid memory leaks
        Destroy(gameObject, lifetime);
    }

    public void Initialize(Vector3 direction)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(direction * speed, ForceMode.Impulse);
            rb.AddForce(Vector3.up * upwardForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Handle collision with the player or other objects
        PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }

        // Destroy the projectile after hitting something
        Destroy(gameObject);
    }
}
