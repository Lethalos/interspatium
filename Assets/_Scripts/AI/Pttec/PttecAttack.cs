using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PttecAttack : MonoBehaviour
{
    public float attackRange = 10f; // Range within which the pet can attack enemies
    public List<Transform> firePoints; // List of points from which projectiles are fired
    public ObjectPool projectilePool; // Reference to the object pool
    public GameObject burstEffectPrefab; // Reference to the burst effect prefab
    public float rotationSpeed = 5f; // Speed of rotation towards the target
    public float staggerTime = 1f; // Time difference between firing from different points

    private Transform currentTarget;
    private float lastAttackTime;
    private int nextFirePointIndex = 0;

    private bool isFiring = false;
    void Start()
    {
        StartFiring();
    }
    void Update()
    {
        // Find the nearest enemy
        FindNearestEnemy();

        // Rotate towards the current target if there is one
        if (currentTarget != null)
        {
            RotateTowardsTarget();
        }
    }

    void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        Transform nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy.transform;
            }
        }

        currentTarget = nearestEnemy;
    }

    void RotateTowardsTarget()
    {
        // Calculate the direction to the target
        Vector3 direction = (currentTarget.position - transform.position).normalized;

        // Calculate the rotation required to look at the target
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        // Smoothly rotate towards the target
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    void StartFiring()
    {
        if (!isFiring)
        {
            isFiring = true;
            InvokeRepeating("FireNextProjectile", 0f, staggerTime);
        }
    }

    void FireNextProjectile()
    {
        if (firePoints != null && firePoints.Count > 0 && currentTarget != null)
        {
            // Fire from the current fire point
            FireProjectile(firePoints[nextFirePointIndex]);

            // Schedule the next fire point
            nextFirePointIndex = (nextFirePointIndex + 1) % firePoints.Count;
        }
    }

    void FireProjectile(Transform firePoint)
    {
        // Instantiate and play the burst effect at the fire point
        if (burstEffectPrefab != null)
        {
            GameObject burstEffect = Instantiate(burstEffectPrefab, firePoint.position, firePoint.rotation);
            ParticleSystem particleSystem = burstEffect.GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                particleSystem.Play();
                Destroy(burstEffect, particleSystem.main.duration);
            }
            else
            {
                Destroy(burstEffect, 1f); // Destroy after 1 second if no particle system is found
            }
        }

        // Get a projectile from the pool and set its target
        GameObject projectile = projectilePool.GetObject();
        projectile.transform.position = firePoint.position;
        projectile.transform.rotation = firePoint.rotation;

        PttecProjectile projectileScript = projectile.GetComponent<PttecProjectile>();
        if (projectileScript != null)
        {
            projectileScript.SetTarget(currentTarget, projectilePool);
        }
    }
}
