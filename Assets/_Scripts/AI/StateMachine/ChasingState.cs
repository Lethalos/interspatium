using UnityEngine;

public class ChasingState : EnemyState
{
    public ChasingState(EnemyBehavior enemy) : base(enemy) { }

    public override void OnEnter()
    {
        enemy.GetComponent<EnemyBehavior>().ChangeAnimation("Chasing");
        enemy.agent.speed = 10;
    }

    public override void Tick()
    {
        if (enemy.player == null) return;

        enemy.agent.SetDestination(enemy.player.position);

        enemy.CheckPlayerState();

        if (!enemy.playerInSightRange)
        {
            enemy.SetState(new PatrollingState(enemy));
        }
        else if (enemy.playerInAttackRange)
        {
            enemy.SetState(enemy is MeleeEnemy ? (IState)new MeleeAttackState(enemy as MeleeEnemy) : new RangedAttackState(enemy as RangedEnemy));
        }
    }
}
