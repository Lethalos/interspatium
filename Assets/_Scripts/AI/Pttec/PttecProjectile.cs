using UnityEngine;

public class PttecProjectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;
    public GameObject areaDamagePrefab; // Reference to the area damage effect prefab

    private Transform target;
    private Vector3 lastKnownPosition;
    private bool targetDestroyed = false;
    private ObjectPool objectPool;

    public void SetTarget(Transform target, ObjectPool pool)
    {
        this.target = target;
        this.objectPool = pool;
        if (target != null)
        {
            lastKnownPosition = target.position;
            targetDestroyed = false;
        }
    }

    void Update()
    {
        if (target == null)
        {
            if (!targetDestroyed)
            {
                targetDestroyed = true;
                // Save the last known position of the target
                lastKnownPosition = lastKnownPosition;
            }
            else
            {
                MoveToLastKnownPosition();
                return;
            }
        }
        else
        {
            // Update the last known position of the target while it is still alive
            lastKnownPosition = target.position;
        }

        MoveTowards(targetDestroyed ? lastKnownPosition : target.position);
    }

    void MoveToLastKnownPosition()
    {
        MoveTowards(lastKnownPosition);
    }

    void MoveTowards(Vector3 destination)
    {
        Vector3 direction = destination - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        if (!targetDestroyed && target != null)
        {
            // Assuming the enemy has a script with a method "TakeDamage"
            EnemyBehavior enemy = target.GetComponent<EnemyBehavior>();
            if (enemy != null)
            {
                enemy.KillEnemy();
            }
        }

        CreateAreaDamageEffect();
        ReturnToPool();
    }

    void CreateAreaDamageEffect()
    {
        // Instantiate the area damage effect at the last known position with -90 degrees rotation on the X-axis
        if (areaDamagePrefab != null)
        {
            Instantiate(areaDamagePrefab, lastKnownPosition, Quaternion.Euler(-90, 0, 0));
        }
    }

    void ReturnToPool()
    {
        objectPool.ReturnObject(gameObject);
    }
}