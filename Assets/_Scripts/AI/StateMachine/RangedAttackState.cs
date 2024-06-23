using UnityEngine;

public class RangedAttackState : EnemyState
{
    private RangedEnemy rangedEnemy;

    public RangedAttackState(RangedEnemy enemy) : base(enemy)
    {
        rangedEnemy = enemy;
    }

    public override void OnEnter()
    {
        if (rangedEnemy.player == null) return;

        rangedEnemy.agent.SetDestination(rangedEnemy.transform.position);
        rangedEnemy.transform.LookAt(rangedEnemy.player);

        //rangedEnemy.GetComponent<EnemyBehavior>().ChangeAnimation("ShootProjectile");
        //Vector3 directionToPlayer = (rangedEnemy.player.position - rangedEnemy.transform.position).normalized;
        //Vector3 spawnPosition = rangedEnemy.transform.position + rangedEnemy.transform.forward + rangedEnemy.projectileOffset;
        //GameObject proj = Object.Instantiate(rangedEnemy.projectile, spawnPosition, Quaternion.identity);
        //proj.GetComponent<Projectile>().Initialize(directionToPlayer);
    }

    public override void Tick()
    {
        if (rangedEnemy.player == null) return;

        if (!rangedEnemy.alreadyAttacked)
        {
            //if (rangedEnemy.HasClearLineOfSight())
            {
                rangedEnemy.GetComponent<EnemyBehavior>().ChangeAnimation("ShootProjectile");
                rangedEnemy.transform.LookAt(rangedEnemy.player);
                Vector3 directionToPlayer = (rangedEnemy.player.position - rangedEnemy.transform.position).normalized;
                Vector3 spawnPosition = rangedEnemy.transform.position + rangedEnemy.transform.forward + rangedEnemy.projectileOffset;
                GameObject proj = Object.Instantiate(rangedEnemy.projectile, spawnPosition, Quaternion.identity);
                proj.GetComponent<Projectile>().Initialize(directionToPlayer);

                rangedEnemy.alreadyAttacked = true;
                rangedEnemy.Invoke(nameof(rangedEnemy.ResetAttack), rangedEnemy.timeBetweenAttacks);
            }
            //else
            {
                rangedEnemy.MoveToBetterPosition();
            }
        }

        rangedEnemy.CheckPlayerState();

        if (!rangedEnemy.playerInAttackRange)
        {
            rangedEnemy.SetState(new ChasingState(rangedEnemy));
        }
    }
}
