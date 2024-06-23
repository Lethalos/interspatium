using UnityEngine;

public class MeleeEnemy : EnemyBehavior
{
    public int meleeAttackDamage;

    protected override void Start()
    {
        base.Start();
        SetState(new PatrollingState(this));
    }

    public void MeleeAttack()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(meleeAttackDamage);
        }
    }

    public void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
