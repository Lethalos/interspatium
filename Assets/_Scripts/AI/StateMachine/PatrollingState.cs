using UnityEngine;
using System.Collections;

public class PatrollingState : EnemyState
{
    public PatrollingState(EnemyBehavior enemy) : base(enemy) { }

    public override void OnEnter()
    {
        enemy.GetComponent<EnemyBehavior>().ChangeAnimation("Walking");
        enemy.agent.speed = 5;
        SearchWalkPoint();
    }

    public override void Tick()
    {
        if (!enemy.walkPointSet) SearchWalkPoint();

        if (enemy.walkPointSet)
            enemy.agent.SetDestination(enemy.walkPoint);

        Vector3 distanceToWalkPoint = enemy.transform.position - enemy.walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            enemy.walkPointSet = false;
            enemy.StartCoroutine(WaitAtWalkPoint());
        }

        enemy.CheckPlayerState();

        if (enemy.playerInSightRange)
        {
            enemy.SetState(new ChasingState(enemy));
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-enemy.walkPointRange, enemy.walkPointRange);
        float randomX = Random.Range(-enemy.walkPointRange, enemy.walkPointRange);

        enemy.walkPoint = new Vector3(enemy.transform.position.x + randomX, enemy.transform.position.y, enemy.transform.position.z + randomZ);

        if (Physics.Raycast(enemy.walkPoint, -enemy.transform.up, 5f, enemy.whatIsGround))
            enemy.walkPointSet = true;
    }

    private IEnumerator WaitAtWalkPoint()
    {
        enemy.isWaiting = true;
        enemy.GetComponent<EnemyBehavior>().ChangeAnimation("Idle");
        yield return new WaitForSeconds(3f);
        enemy.isWaiting = false;
    }
}
