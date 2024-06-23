using UnityEngine;
using UnityEngine.AI;

public class RangedEnemy : EnemyBehavior
{
    public GameObject projectile;
    public LayerMask whatIsObstacle;
    public Vector3 projectileOffset = new Vector3(0, 1.5f, 0);

    protected override void Start()
    {
        base.Start();
        SetState(new PatrollingState(this));
    }

    public bool HasClearLineOfSight()
    {
        if (player == null) return false;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, (player.position - transform.position).normalized, out hit, attackRange))
        {
            if (hit.transform == player)
            {
                return true; // Clear line of sight
            }
        }
        return false; // Obstacle detected
    }

    public void MoveToBetterPosition()
    {
        if (player == null) return;

        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        Vector3 newPosition = transform.position + directionToPlayer * 5f; // Move 5 units closer to the player

        NavMeshHit hit;
        if (NavMesh.SamplePosition(newPosition, out hit, 5f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    public void ResetAttack()
    {
        alreadyAttacked = false;
    }
}