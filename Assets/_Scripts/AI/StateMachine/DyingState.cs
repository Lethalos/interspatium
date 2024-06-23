using UnityEngine;

public class DyingState : EnemyState
{
    public DyingState(EnemyBehavior enemy) : base(enemy) { }

    public override void OnEnter()
    {
        enemy.GetComponent<EnemyBehavior>().ChangeAnimation("Dying");
        enemy.agent.SetDestination(enemy.transform.position);
        enemy.agent.isStopped = true;
    }

    public override void Tick() { }

    public override void OnExit()
    {
    }
}
