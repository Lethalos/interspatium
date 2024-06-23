using UnityEngine;

public abstract class EnemyState : IState
{
    protected EnemyBehavior enemy;

    public EnemyState(EnemyBehavior enemy)
    {
        this.enemy = enemy;
    }

    public virtual void OnEnter() { }
    public virtual void Tick() { }
    public virtual void OnExit() { }
}
