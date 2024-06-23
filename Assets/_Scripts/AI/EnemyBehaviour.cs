using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBehavior : AiStateMachine
{
    public Animator animator;
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    // Patrolling
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;
    public bool isWaiting = false;

    // Attacking
    public float timeBetweenAttacks;
    public bool alreadyAttacked;

    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Subscribe to the player death event
        PlayerHealth.OnPlayerDeath += HandlePlayerDeath;
    }

    protected virtual void OnDestroy()
    {
        // Unsubscribe from the player death event to avoid memory leaks
        PlayerHealth.OnPlayerDeath -= HandlePlayerDeath;
    }

    public void CheckPlayerState()
    {
        if (player == null || !player.gameObject.activeInHierarchy)
        {
            playerInSightRange = false;
            playerInAttackRange = false;
            return;
        }

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
    }

    public void KillEnemy()
    {
        // Disable the NavMeshAgent
        agent.enabled = false;

        // Disable the EnemyBehavior script
        this.enabled = false;

        // Disable the collider
        GetComponent<BoxCollider>().enabled = false;

        // Disable the GameObject
        ChangeAnimation("Death");

        gameObject.tag = "Untagged";
    }
         
    private string currentAnimation = "Idle";

    public void ChangeAnimation(string animation, float crossFade = 0.2f)
    {
        if(currentAnimation == animation) return;

        animator.CrossFade(animation, crossFade);
    }

    public void ChangeAnimationWithDelay(string animation, float delay, float crossFade = 0.2f)
    {
        if(currentAnimation == animation) return;

        StartCoroutine(DelayedAnimationChange(animation, delay, crossFade));
    }

    private IEnumerator DelayedAnimationChange(string animation, float delay, float crossFade)
    {
        yield return new WaitForSeconds(delay);
        ChangeAnimation(animation, crossFade);
    }

    private void HandlePlayerDeath()
    {
        // Change state to Patrolling when the player dies
        SetState(new PatrollingState(this));
    }
}
