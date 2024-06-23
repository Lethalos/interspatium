using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PttecController : MonoBehaviour
{
    public Vector3 offset = new Vector3(0.5f, 1.5f, 0f); // Offset position from the player
    public float followSpeed = 2f; // Base speed of following
    public float maxSpeed = 5f; // Maximum speed the pet can reach
    public float acceleration = 0.5f; // Acceleration rate
    public float swingAmplitude = 0.5f; // Amplitude of the swing
    public float swingFrequency = 1f; // Frequency of the swing
    public float enemyDetectionRange = 10f; // Range to detect enemies

    private Transform player;
    private Vector3 currentVelocity = Vector3.zero; // Current velocity of the pet
    private float originalY; // Original Y position for swinging
    private bool enemyInRange = false; // Flag to check if an enemy is in range

    void Start()
    {
        FindPlayer();
        originalY = transform.position.y;
    }

    void Update()
    {
        if (player == null)
        {
            FindPlayer();
        }

        if (player != null)
        {
            // Calculate the target position
            Vector3 targetPosition = player.position + player.TransformVector(offset);
            FollowPlayer(targetPosition);
        }

        // Check for enemies in range
        CheckForEnemies();

        if (!enemyInRange)
        {
            // Apply swinging motion when no enemies are in range
            ApplySwingingMotion();
        }
    }

    void FindPlayer()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void FollowPlayer(Vector3 targetPosition)
    {
        // Calculate the direction towards the target position
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Gradually increase speed towards the target position
        currentVelocity += direction * acceleration * Time.deltaTime;

        // Clamp the velocity to the maximum speed
        currentVelocity = Vector3.ClampMagnitude(currentVelocity, maxSpeed);

        // Move the pet towards the target position
        transform.position += currentVelocity * Time.deltaTime;
    }

    void ApplySwingingMotion()
    {
        // Apply vertical swinging motion
        Vector3 position = transform.position;
        position.y = originalY + Mathf.Sin(Time.time * swingFrequency) * swingAmplitude;
        transform.position = position;
    }

    void CheckForEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyInRange = false;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy <= enemyDetectionRange)
            {
                enemyInRange = true;
                break;
            }
        }
    }
}
