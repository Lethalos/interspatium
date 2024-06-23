using UnityEngine;

public class MeleeAttackState : EnemyState
{
    private MeleeEnemy meleeEnemy;

    public MeleeAttackState(MeleeEnemy enemy) : base(enemy)
    {
        meleeEnemy = enemy;
    }

    public override void OnEnter()
    {
        if (meleeEnemy.player == null) return;

        meleeEnemy.agent.SetDestination(meleeEnemy.transform.position);
        meleeEnemy.transform.LookAt(meleeEnemy.player);
        meleeEnemy.GetComponent<EnemyBehavior>().ChangeAnimation("Attack");
    }

    public override void Tick()
    {
        if (meleeEnemy.player == null) return;

        if (!meleeEnemy.alreadyAttacked)
        {
            if (Vector3.Distance(meleeEnemy.transform.position, meleeEnemy.player.position) <= meleeEnemy.attackRange)
            {
                meleeEnemy.MeleeAttack();
                meleeEnemy.alreadyAttacked = true;
                meleeEnemy.Invoke(nameof(meleeEnemy.ResetAttack), meleeEnemy.timeBetweenAttacks);
            }
        }

        meleeEnemy.CheckPlayerState();

        if (!meleeEnemy.playerInAttackRange)
        {
            meleeEnemy.SetState(new ChasingState(meleeEnemy));
        }
    }
}
